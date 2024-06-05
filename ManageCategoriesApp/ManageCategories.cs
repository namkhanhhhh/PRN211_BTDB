using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ManageCategoriesApp
{
    public record Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
        public class ManageCategories
        {
            SqlConnection connection;
            SqlCommand command;
            string ConnectionString = "Server=LAPCUAKHANHH\\MSSQLSERVER01;uid=sa;pwd=12345;database=MyStore";
            public List<Category> GetCategories()
            {
                List<Category> categories = new List<Category>();
                connection = new SqlConnection(ConnectionString);
                string sql = "Select CategoryID, CategoryName from Categories";
                command=new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryID = reader.GetInt32("CategoryID"),
                                CategoryName = reader.GetString("CategoryName")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return categories;
            }
            public void InsertCategory(Category category)
            {
                connection = new SqlConnection(ConnectionString);
                command = new SqlCommand("Insert categories values(@CategoryName)", connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                }
            public void UpdateCategory(Category category)
            {
            connection = new SqlConnection(ConnectionString);
                string sql = "Update Categories set CategoryName=@CategoryName where CategoryID=@CategoryID";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CategoryID",category.CategoryID);
                command.Parameters.AddWithValue("@CategoryName",category.CategoryName);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            public void DeleteCategory(Category category)
            {
                connection = new SqlConnection(ConnectionString);
                string sql = "Delete Categories where CategoryID=@CategoryID";
                command=new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            }
        }
