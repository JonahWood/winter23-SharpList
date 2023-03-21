using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sharpList.Repositories
{
    public class HousesRepository
    {
          private readonly IDbConnection _db;

  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }

        internal List<House> FindAll()
        {
                string sql = "SELECT * FROM houses;";
    List<House> houses = _db.Query<House>(sql).ToList();
    return houses;
        }

        internal House FindOne(int id)
        {
              {
    string sql = @"
    SELECT 
    *
    FROM houses
    WHERE id = @id;
    ";
    House house = _db.Query<House>(sql, new { id }).FirstOrDefault();
    return house;
  }
        }

        internal bool Remove(int id)
        {
              {
    string sql = @"
    DELETE FROM houses WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows == 1;
  }
        }

          internal House Create(House houseData)
  {
    string sql = @"
    INSERT INTO houses
    (bedrooms, bathrooms, description, imgUrl, price, year)
    VALUES
    (@bedrooms, @bathrooms, @description, @imgUrl, @price, @year);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, houseData);
    houseData.Id = id;
    return houseData;
  }
    }
}