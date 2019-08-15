using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Core.Extensions;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Core.Service
{
    public class CoreService
    {
        public CoreService() { }
        public CoreService(HttpContext Context) { }

        public Facility GetFacility(string code) {
            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT fc_idnt, fc_code, fc_name, fc_geolocation, fc_description, fc_genexpert, fc_xray, fc_treatment FROM Facilities WHERE fc_code LIKE '" + code + "'");
            if (dr.Read()) {
                return new Facility {
                    Id = Convert.ToInt64(dr[0]),
                    Code = dr[1].ToString(),
                    Name = dr[2].ToString(),
                    GeoLocation = dr[3].ToString(),
                    Description = dr[4].ToString(),
                    GXpt = Convert.ToBoolean(dr[5]),
                    XRay = Convert.ToBoolean(dr[6]),
                    Tbc = Convert.ToBoolean(dr[7])
                };
            }

            return null;
        }

        public List<Facility> GetFacilities() {
            List<Facility> facilities = new List<Facility>();

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT fc_idnt, fc_code, fc_name, fc_geolocation, fc_description, fc_genexpert, fc_xray, fc_treatment FROM Facilities ORDER BY fc_name ");
            if (dr.HasRows) {
                while (dr.Read()) {
                    facilities.Add(new Facility {
                        Id = Convert.ToInt64(dr[0]),
                        Code = dr[1].ToString(),
                        Name = dr[2].ToString(),
                        GeoLocation = dr[3].ToString(),
                        Description = dr[4].ToString(),
                        GXpt = Convert.ToBoolean(dr[5]),
                        XRay = Convert.ToBoolean(dr[6]),
                        Tbc = Convert.ToBoolean(dr[7])
                    });
                }
            }

            return facilities;
        }

        public List<SelectListItem> GetIEnumerable(string query) {
            List<SelectListItem> enumarables = new List<SelectListItem>();
            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect(query);
            if (dr.HasRows) {
                while (dr.Read()) {
                    enumarables.Add(new SelectListItem {
                        Value = dr[0].ToString(),
                        Text = dr[1].ToString()
                    });
                }
            }

            return enumarables;
        }

        public JObject GetAutocomplete(string query) {
            string accounts = "{";

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect(query);
            if (dr.HasRows) {
                while (dr.Read()) {
                    accounts += "'" + dr[0].ToString().Replace("'", "`").Replace(":", "") + "': null, ";
                }
            }

            return JObject.Parse(accounts + "}");
        }
    }
}
