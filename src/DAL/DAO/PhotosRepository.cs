using DAL.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using MySql.Data.MySqlClient;
using DAL.DAO.Base;
using System.IO;

namespace DAL.DAO
{
    public class PhotosRepository : IPhotoRepository
    {

        public int Create(Photo t)
        {
            string query = string.Format(
                @"INSERT INTO test.photos (Creator, DateAndTime, Path, DefectId)
                  VALUES ('{0}', '{1}', '{2}', {3});
                  SELECT * FROM test.photos WHERE Creator='{0}' AND DateAndTime='{1}' AND
                  Path='{2}' AND DefectId={3}", t.Creator, t.CreatedAt, t.Path, t.DefectId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows ? int.Parse(reader["Id"].ToString()) : -1;
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format(
                @"DELETE FROM test.photos WHERE Id={0}; SELECT * FROM test.photos WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return !(reader.Read() && reader.HasRows);
            }
        }

        public bool DeletePhotosOfDefect(int defectId)
        {
            string query = string.Format(
                @"DELETE FROM test.photos WHERE DefectId={0};
                  SELECT * FROM test.photos WHERE DefectId={0}", defectId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return !(reader.Read() && reader.HasRows);
            }
        }

        public bool Exists(Photo t)
        {
            string query = string.Format(
                @"SELECT * FROM test.photos WHERE Creator='{0}' AND DateAndTime='{1}' AND
                  Path='{2}' AND DefectId={3}", t.Creator, t.CreatedAt, t.Path, t.DefectId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public Photo Get(int id)
        {
            string query = string.Format("SELECT * FROM test.photos WHERE Id={0}");
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return new Photo(reader);
            }
        }

        public List<Photo> GetAll()
        {
            string query = string.Format("SELECT * FROM test.photos");
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<Photo> photos = new List<Photo>();
                while (reader.Read() && reader.HasRows)
                {
                    photos.Add(new Photo(reader));
                }
                return photos;
            }
        }

        public List<Photo> GetPhotosByDefectId(int defectId)
        {
            string query = string.Format(
                @"SELECT * FROM test.photos WHERE DefectId={0}", defectId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<Photo> photos = new List<Photo>();
                while (reader.Read() && reader.HasRows)
                {
                    photos.Add(new Photo(reader));
                }
                return photos;
            }
        }

        public bool Update(int id, Photo t)
        {
            string query = string.Format(
                @"UPDATE test.photos SET Creator='{0}', DateAndTime='{1}', Path='{2}', DefectId={3}
                  WHERE Id='{4}';
                  SELECT * FROM test.photos WHERE Creator='{0}' AND DateAndTime='{1}' AND Path='{2}' AND
                  DefectId={3}", t.Creator, t.CreatedAt, t.Path, t.DefectId, id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }
    }
}
