using DAL.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using MySql.Data.MySqlClient;
using DAL.DAO.Base;

namespace DAL.DAO
{
    public class MaterialsRepository : IMaterialRepository
    {
        private readonly IMeasureUnitRepository measureUnitRepository;
        public MaterialsRepository(IMeasureUnitRepository measureUnitRepository)
        {
            this.measureUnitRepository = measureUnitRepository;
        }
        public int Create(Material t)
        {
            string query = string.Format(
                @"INSERT INTO test.materials (Title, Amount, MeasureUnitId) VALUES ('{0}', {1}, {2}); 
                SELECT * FROM test.materials WHERE Title='{0}' AND Amount={1} AND MeasureUnitId='{2}'", t.Title, t.Amount, t.MeasureUnitId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows ? 1 : -1;
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format("DELETE FROM test.materials WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return !(reader.Read() && reader.HasRows);
            }
        }

        public bool Exists(Material t)
        {
            string query = string.Format(
                "SELECT * FROM test.materials WHERE Title='{0}' AND Amount={1} AND MeasureUnitId={2}", t.Title, t.Amount, t.MeasureUnitId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public Material Get(int id)
        {
            string query = string.Format("SELECT * FROM test.materials WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    Material material = new Material(reader);
                    material.MeasureUnit = measureUnitRepository.Get(material.MeasureUnitId);
                    return material;
                }
                return null;
            }
        }

        public List<Material> GetAll()
        {
            string query = string.Format("SELECT * FROM test.materials");
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<Material> materials = new List<Material>();
                while (reader.Read() && reader.HasRows)
                {
                    Material material = new Material(reader);
                    material.MeasureUnit = measureUnitRepository.Get(material.MeasureUnitId);
                    materials.Add(material);
                }
                return materials;
            }
        }

        public bool Update(int id, Material t)
        {
            string query = string.Format(
                @"UPDATE test.materials SET Title='{0}', Amount={1}, MeasureUnitId={2} WHERE Id={3};
                SELECT * FROM test.materials WHERE Title='{0}' AND Amount={1} AND MeasureUnitId={2}",
                t.Title, t.Amount, t.MeasureUnitId, id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }
    }
}
