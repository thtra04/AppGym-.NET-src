using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class QuyenTaiKhoanDAO
    {
        public List<QuyenTaiKhoan> GetByTaiKhoan(int maTK)
        {
            var list = new List<QuyenTaiKhoan>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT MaTK, Module, CanView, CanAdd, CanEdit, CanDelete FROM QuyenTaiKhoan WHERE MaTK=@MaTK", conn);
            cmd.Parameters.AddWithValue("@MaTK", maTK);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new QuyenTaiKhoan
                {
                    MaTK = reader.GetInt32(0),
                    Module = reader.GetString(1),
                    CanView = reader.GetBoolean(2),
                    CanAdd = reader.GetBoolean(3),
                    CanEdit = reader.GetBoolean(4),
                    CanDelete = reader.GetBoolean(5)
                });
            }
            return list;
        }

        public void SaveAll(int maTK, IEnumerable<QuyenTaiKhoan> quyens)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try
            {
                using (var del = new SqlCommand("DELETE FROM QuyenTaiKhoan WHERE MaTK=@MaTK", conn, tran))
                {
                    del.Parameters.AddWithValue("@MaTK", maTK);
                    del.ExecuteNonQuery();
                }

                foreach (var q in quyens)
                {
                    if (!q.CanView && !q.CanAdd && !q.CanEdit && !q.CanDelete) continue;
                    using var ins = new SqlCommand(
                        @"INSERT INTO QuyenTaiKhoan (MaTK, Module, CanView, CanAdd, CanEdit, CanDelete)
                          VALUES (@MaTK, @Module, @CanView, @CanAdd, @CanEdit, @CanDelete)", conn, tran);
                    ins.Parameters.AddWithValue("@MaTK", maTK);
                    ins.Parameters.AddWithValue("@Module", q.Module);
                    ins.Parameters.AddWithValue("@CanView", q.CanView);
                    ins.Parameters.AddWithValue("@CanAdd", q.CanAdd);
                    ins.Parameters.AddWithValue("@CanEdit", q.CanEdit);
                    ins.Parameters.AddWithValue("@CanDelete", q.CanDelete);
                    ins.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
