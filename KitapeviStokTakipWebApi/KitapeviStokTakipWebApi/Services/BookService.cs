using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace KitapeviStokTakipWebApi.Services
{
    public class BookService : IBookService
    {
        private readonly string _connectionString;

        public BookService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Select * From BOOKS";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            BookId = Convert.ToInt32(reader["BOOK_ID"]),
                            BookName = reader["BOOK_NAME"].ToString(),
                            Writer = reader["WRITER"].ToString(),
                            Price = Convert.ToInt32(reader["PRICE"]),
                            StockAmount = Convert.ToInt32(reader["STOCK_AMOUNT"]),
                            Barkod = reader["BARKOD"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CATEGORY_ID"]),
                            AddedDate = reader["ADDED_DATE"].ToString()
                        };

                        books.Add(book);
                    }

                }
            }
            return books;
        }

        public Book GetBookById(int id)
        {
            Book book = new Book();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Select * From BOOKS Where BOOK_ID = :id";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        book = new Book
                        {
                            BookId = Convert.ToInt32(reader["BOOK_ID"]),
                            BookName = reader["BOOK_NAME"].ToString(),
                            Writer = reader["WRITER"].ToString(),
                            Price = Convert.ToInt32(reader["PRICE"]),
                            StockAmount = Convert.ToInt32(reader["STOCK_AMOUNT"]),
                            Barkod = reader["BARKOD"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CATEGORY_ID"]),
                            AddedDate = reader["ADDED_DATE"].ToString()
                        };
                    }
                }
            }

            return book;
        }

        public Book AddBook(Book book)
        {

            try
            {

                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        OracleParameter[] prm = new OracleParameter[6];

                        prm[0] = command.Parameters.Add("categoryId", OracleDbType.Decimal,
                           book.CategoryId, ParameterDirection.Input);
                        prm[1] = command.Parameters.Add("bookName", OracleDbType.Varchar2,
                          book.BookName, ParameterDirection.Input);
                        prm[2] = command.Parameters.Add("writer", OracleDbType.Varchar2,
                          book.Writer, ParameterDirection.Input);
                        prm[3] = command.Parameters.Add("price", OracleDbType.Decimal,
                          book.Price, ParameterDirection.Input);
                        prm[4] = command.Parameters.Add("stockAmount", OracleDbType.Decimal,
                          book.StockAmount, ParameterDirection.Input);
                        prm[5] = command.Parameters.Add("barkod", OracleDbType.Varchar2,
                          book.Barkod, ParameterDirection.Input);
                        

                        var sql = "INSERT INTO BOOKS(" +
                            "CATEGORY_ID," +
                            "BOOK_NAME," +
                            "WRITER," +
                            "PRICE," +
                            "STOCK_AMOUNT," +
                            "BARKOD," +
                            "ADDED_DATE) " +
                            "VALUES(:categoryId, :bookName, :writer, :price, :stockAmount, :barkod, TO_CHAR(sysdate,'DD/MM/YYYY HH24:MI:SS'))";

                        command.BindByName = true;
                        command.CommandText = sql;
                        command.ExecuteNonQuery();


                    }
                }
            }
            catch
            {
                throw;
            }
            return book;
        }

        public Book UpdateBook(Book book)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {
                       


                        connection.Open();

                        command.BindByName = true;

                        command.Parameters.Add("id", book.BookId);
                        command.Parameters.Add("bookName", book.BookName);
                        command.Parameters.Add("writer", book.Writer);
                        command.Parameters.Add("price", book.Price);
                        command.Parameters.Add("stockAmount", book.StockAmount);
                        command.Parameters.Add("barkod", book.Barkod);
                        command.Parameters.Add("categoryId", book.CategoryId);
                

                        command.CommandText = "Update BOOKS Set BOOK_NAME = :bookName," +
                            " WRITER = :writer," +
                            " PRICE = :price," +
                            " STOCK_AMOUNT = :stockAmount," +
                            " BARKOD = :barkod," +
                            " CATEGORY_ID = :categoryId," +
                            " ADDED_DATE = TO_CHAR(sysdate,'DD/MM/YYYY HH24:MI:SS') " +
                            " Where BOOK_ID = :id";


                        command.ExecuteNonQuery();

                    }
                }
            }
            catch
            {
                throw;
            }
            return book;
        }

        public int DeleteBook(int id)
        {

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.Parameters.Add("id", id);
                    command.CommandText = "Delete From BOOKS Where BOOK_ID = :id";
                    command.ExecuteNonQuery();

                }
            }
            return id;
        }
    }
}
