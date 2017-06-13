using DAL.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using DAL.DAO.Base;
using MySql.Data.MySqlClient;
using System.Linq;

namespace DAL.DAO
{
    public class DefectsRepository : IDefectRepository
    {

        private readonly IPhotoRepository photoRepository;

        public DefectsRepository(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }

        public int Create(Defect t)
        {
            string query = string.Format(
                @"INSERT INTO test.defects (DateAndTime, TermDateAndTime, IsFixed)
                  VALUES ('{0}', '{1}', {2});
                  SELECT * FROM test.defects WHERE DateAndTime='{0}' AND 
                  TermDateAndTime='{1}' AND IsFixed={2}", t.DateCreated.ToString(Constants.DateTime),
                t.TermDateTime.ToString(Constants.DateTime), t.IsFixed);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    Defect defect = new Defect(reader);
                    foreach(Photo photo in t.Photos)
                    {
                        photo.DefectId = defect.Id;
                        photoRepository.Create(photo);
                    }
                    return 1;
                }
                return -1;
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format(
                @"DELETE FROM test.defects WHERE Id={0}; SELECT * FROM test.defects WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return photoRepository.DeletePhotosOfDefect(id) && !(reader.Read() && reader.HasRows);
            }
        }

        public bool Exists(Defect t)
        {
            string query = string.Format(
                @"SELECT * FROM test.defects WHERE DateAndTime='{0}' AND TermDateAndTime='{1}' AND
                  IsFixed={2}", t.DateCreated, t.TermDateTime, t.IsFixed);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public Defect Get(int id)
        {
            string query = string.Format("SELECT * FROM test.defects WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    Defect defect = new Defect(reader);
                    defect.Photos = photoRepository.GetPhotosByDefectId(defect.Id);
                    return defect;
                }
                return null;
            }
        }

        public List<Defect> GetAll()
        {
            string query = string.Format("SELECT * FROM test.defects");
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<Defect> defects = new List<Defect>();
                while (reader.Read() && reader.HasRows)
                {
                    Defect defect = new Defect(reader);
                    defect.Photos = photoRepository.GetPhotosByDefectId(defect.Id);
                    defects.Add(defect);
                }
                return defects;
            }
        }

        public bool Update(int id, Defect t)
        {
            string query = string.Format(
                @"UPDATE test.defects SET DateAndTime='{0}', TermDateAndTime='{1}', 
                  IsFixed={2} WHERE Id={3};
                  SELECT * FROM test.defects WHERE DateAndTime='{0}' AND TermDateAndTime='{1}' AND
                  IsFixed={2}", t.DateCreated.ToString(Constants.DateTime),
                  t.TermDateTime.ToString(Constants.DateTime), t.IsFixed, id); 
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    List<Photo> photosOfPreviousDefects = photoRepository.GetPhotosByDefectId(id).Where(p => !t.Photos.Any(l => l.Id == p.Id)).ToList(); ;
                    List<Photo> deletedPhotos = photosOfPreviousDefects.Where(p => !t.Photos.Any(l => l.Id == p.Id)).ToList();
                    foreach(Photo deletedPhoto in deletedPhotos)
                    {
                        photoRepository.Delete(deletedPhoto.Id);
                    }
                    List<Photo> oldPhotos = photosOfPreviousDefects.Where(p => !t.Photos.Any(l => l.Id == p.Id)).ToList(); 
                    foreach (Photo photo in oldPhotos)
                    {
                        if (photo.Id != 0)
                        {
                           photoRepository.Update(photo.Id, photo);
                        }
                    }
                    return true;
                }
                return false;
            }
        }
    }
}
