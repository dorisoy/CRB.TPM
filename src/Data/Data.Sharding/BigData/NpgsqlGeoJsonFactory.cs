using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System.Data;
using Z.Dapper.Plus;
using Dapper;

namespace CRB.TPM.Data.Sharding
{

    /// <summary>
    /// Npgsql映射GeoJSON数据结构工厂
    /// </summary>
    public class NpgsqlGeoJsonFactory
    {
        public static void UseGeoJson()
        {
            //NpgsqlConnection.GlobalTypeMapper.UseGeoJson();
            DapperPlusManager.AddCustomSupportedType(typeof(Point));
            DapperPlusManager.AddCustomSupportedType(typeof(MultiPoint));
            DapperPlusManager.AddCustomSupportedType(typeof(LineString));
            DapperPlusManager.AddCustomSupportedType(typeof(MultiLineString));
            DapperPlusManager.AddCustomSupportedType(typeof(Polygon));
            DapperPlusManager.AddCustomSupportedType(typeof(MultiPolygon));
            DapperPlusManager.AddCustomSupportedType(typeof(GeometryCollection));
            DapperPlusManager.AddCustomSupportedType(typeof(Feature));
            DapperPlusManager.AddCustomSupportedType(typeof(FeatureCollection));
            DapperPlusManager.AddCustomSupportedType(typeof(GeoJSONObject));

            SqlMapper.AddTypeMap(typeof(Point), DbType.Object);
            SqlMapper.AddTypeMap(typeof(MultiPoint), DbType.Object);
            SqlMapper.AddTypeMap(typeof(LineString), DbType.Object);
            SqlMapper.AddTypeMap(typeof(MultiLineString), DbType.Object);
            SqlMapper.AddTypeMap(typeof(Polygon), DbType.Object);
            SqlMapper.AddTypeMap(typeof(MultiPolygon), DbType.Object);
            SqlMapper.AddTypeMap(typeof(GeometryCollection), DbType.Object);
            SqlMapper.AddTypeMap(typeof(Feature), DbType.Object);
            SqlMapper.AddTypeMap(typeof(FeatureCollection), DbType.Object);
            SqlMapper.AddTypeMap(typeof(GeoJSONObject), DbType.Object);
        }
    }
}
