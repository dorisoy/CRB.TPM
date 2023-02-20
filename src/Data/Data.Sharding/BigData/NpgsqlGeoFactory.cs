using NetTopologySuite.Geometries;
using System.Data;
using Z.Dapper.Plus;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    ///  Npgsql映射GeoJSON数据结构工厂
    /// </summary>
    public class NpgsqlGeoFactory
    {
        public static void UseGeo()
        {
            DapperPlusManager.AddCustomSupportedType(typeof(Point));
            DapperPlusManager.AddCustomSupportedType(typeof(MultiPoint));
            DapperPlusManager.AddCustomSupportedType(typeof(LineString));
            DapperPlusManager.AddCustomSupportedType(typeof(MultiLineString));
            DapperPlusManager.AddCustomSupportedType(typeof(Polygon));
            DapperPlusManager.AddCustomSupportedType(typeof(MultiPolygon));
            DapperPlusManager.AddCustomSupportedType(typeof(Geometry));
            DapperPlusManager.AddCustomSupportedType(typeof(GeometryCollection));

            SqlMapper.AddTypeMap(typeof(Point), DbType.Object);
            SqlMapper.AddTypeMap(typeof(MultiPoint), DbType.Object);
            SqlMapper.AddTypeMap(typeof(LineString), DbType.Object);
            SqlMapper.AddTypeMap(typeof(MultiLineString), DbType.Object);
            SqlMapper.AddTypeMap(typeof(Polygon), DbType.Object);
            SqlMapper.AddTypeMap(typeof(MultiPolygon), DbType.Object);
            SqlMapper.AddTypeMap(typeof(Geometry), DbType.Object);
            SqlMapper.AddTypeMap(typeof(GeometryCollection), DbType.Object);
        }     
    }
}
