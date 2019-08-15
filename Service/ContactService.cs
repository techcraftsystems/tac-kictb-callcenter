using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using Core.Extensions;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Core.Service
{
    public class ContactService
    {
        private readonly CoreService Core = new CoreService();
        private readonly Users User = new Users();

        public ContactService() {}
        public ContactService(HttpContext context){
            User = new Users { Id = int.Parse(context.User.FindFirst(ClaimTypes.Actor).Value) };
        }

        public Contacts GetContact(string uuid) {
            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT ct_idnt, ct_complete, ct_favorite, ct_uuid, ct_names, ct_gender, ct_email, ct_phone, ct_image, ct_description, ct_dob, ct_created_on, ct_created_by FROM Contacts WHERE ct_uuid COLLATE SQL_Latin1_General_CP1_CS_AS LIKE '" + uuid + "' ORDER BY ct_names");
            if (dr.HasRows) {
                while (dr.Read()) {
                    return new Contacts {
                        Id = Convert.ToInt64(dr[0]),
                        Complete = Convert.ToInt32(dr[1]),
                        Favorite = Convert.ToInt32(dr[2]),
                        Uuid = dr[3].ToString(),
                        Name = dr[4].ToString(),
                        Gender = dr[5].ToString(),
                        Email = dr[6].ToString(),
                        Telephone = dr[7].ToString(),
                        Image = dr[8].ToString(),
                        Description = dr[9].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr[10]),
                        AddedOn = Convert.ToDateTime(dr[11]),
                        AddedBy = new Users { Id = Convert.ToInt64(dr[12]) }
                    };
                }
            }

            return null;
        }

        public List<Contacts> GetContacts() {
            List<Contacts> contacts = new List<Contacts>();

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT ct_idnt, ct_complete, ct_favorite, ct_uuid, ct_names, ct_gender, ct_email, ct_phone, ct_image, ct_description, ct_dob, ct_created_on, ct_created_by FROM Contacts WHERE ct_void=0 ORDER BY ct_names");
            if (dr.HasRows) {
                while (dr.Read()) {
                    contacts.Add(new Contacts {
                        Id = Convert.ToInt64(dr[0]),
                        Complete = Convert.ToInt32(dr[1]),
                        Favorite = Convert.ToInt32(dr[2]),
                        Uuid = dr[3].ToString(),
                        Name = dr[4].ToString(),
                        Gender = dr[5].ToString(),
                        Email = dr[6].ToString(),
                        Telephone = dr[7].ToString(),
                        Image = dr[8].ToString(),
                        Description = dr[9].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr[10]),
                        AddedOn = Convert.ToDateTime(dr[11]),
                        AddedBy = new Users { Id = Convert.ToInt64(dr[12]) }
                    });
                }
            }

            return contacts;
        }

        public List<SelectListItem> GetGenderIEnumerable() {
            return new List<SelectListItem> {
                new SelectListItem { Value = "Male", Text = "Male" }, 
                new SelectListItem { Value = "Female", Text = "Female" }
            };
        }

        public List<SelectListItem> GetCallerTypeIEnumerable() {
            return new List<SelectListItem> {
                new SelectListItem { Value = "1", Text = "Self" },
                new SelectListItem { Value = "2", Text = "Referee" }
            };
        }

        public List<SelectListItem> GetRecordSourceIEnumerable() {
            return Core.GetIEnumerable("SELECT rs_idnt, rs_source FROM RecordsSource");
        }

        public List<SelectListItem> GetRecordTypeIEnumerable() {
            return Core.GetIEnumerable("SELECT rt_idnt, rt_name FROM RecordsType");
        }

        public List<SelectListItem> GetReferralTypeIEnumerable() {
            return Core.GetIEnumerable("SELECT rft_idnt, rft_name FROM RefereeCategory WHERE rft_void=0");
        }

        public List<SelectListItem> GetScreeningOutcomesIEnumerable() {
            return Core.GetIEnumerable("SELECT so_idnt, so_outcome FROM ScreeningOutcome");
        }

        public JObject GetCommunityUnitsAutocomplete() {
            return Core.GetAutocomplete("SELECT CAST(cu_idnt AS NVARCHAR)+' '+cu_name cu FROM CommunityUnits");
        }

        public JObject GetFacilitiesAutocomplete() {
            return Core.GetAutocomplete("SELECT fc_code+' '+fc_name FROM Facilities ORDER BY fc_name");
        }

        public JObject GetOtherConditionsAutocomplete() {
            return Core.GetAutocomplete("SELECT DISTINCT TOP(200) sc_othercondition FROM Screening WHERE sc_outcome=2");
        }

        public JObject GetRefereesAutocomplete(int catg = -1) {
            string query = "";
            if (!catg.Equals(-1))
                query = "WHERE rf_catg=" + catg;
            return Core.GetAutocomplete("SELECT CAST(rf_idnt AS NVARCHAR)+' '+rf_name FROM Referee " + query);
        }

        /* WRITE DATA */
        public Contacts SaveContact(Contacts contact) {
            SqlServerConnection conn = new SqlServerConnection();
            contact.Id = conn.SqlServerUpdate("DECLARE @idnt INT=" + contact.Id + ", @name NVARCHAR(MAX)='" + contact.Name + "', @dob DATE='" + contact.DateOfBirth + "', @gender NVARCHAR(10)='" + contact.Gender + "', @email NVARCHAR(250)='" + contact.Email + "', @phone NVARCHAR(250)='" + contact.Telephone + "', @physical NVARCHAR(MAX)='" + contact.PhysicalAddress + "', @user INT=" + User.Id + "; IF NOT EXISTS (SELECT ct_idnt FROM Contacts WHERE ct_idnt=@idnt) BEGIN INSERT INTO Contacts (ct_names, ct_dob, ct_gender, ct_email, ct_phone, ct_physical, ct_created_by) output INSERTED.ct_idnt VALUES (@name, @dob, @gender, @email, @phone, @physical, @user) END ELSE BEGIN UPDATE Contacts SET ct_names=@name, ct_dob=@dob, ct_gender=@gender, ct_email=@email, ct_phone=@phone, ct_physical=@physical output INSERTED.ct_idnt WHERE ct_idnt=@idnt END");

            return contact;
        }

        public Record SaveRecord(Record record) {
            SqlServerConnection conn = new SqlServerConnection();
            record.Id = conn.SqlServerUpdate("DECLARE @idnt INT=" + record.Id + ", @contact INT=" + record.Contact.Id + ", @source INT=" + record.Source.Id + ", @type INT=" + record.Type.Id + ", @caller INT=" + record.Caller.Id + ", @caller_name NVARCHAR(250)='" + record.CallerName + "', @caller_phone NVARCHAR(250)='" + record.CallerPhone + "', @referral INT=" + record.Referral.Id + ", @referee INT=" + record.Referee.Id + ", @unit INT=" + record.Referee.Id + ", @summary NVARCHAR(MAX)='" + record.Description + "', @user INT=" + User.Id + "; IF NOT EXISTS (SELECT rc_idnt FROM Records WHERE rc_idnt=@idnt) BEGIN INSERT INTO Records (rc_contact, rc_source, rc_type, rc_caller, rc_caller_name, rc_caller_phone, rc_referral, rc_referee, rc_unit, rc_summary, rc_created_by) output INSERTED.rc_idnt VALUES (@contact, @source, @type, @caller, @caller_name, @caller_phone, @referral, @referee, @unit, @summary, @user) END ELSE BEGIN UPDATE Records SET rc_source=@source, rc_type=@type, rc_caller=@caller, rc_caller_name=@caller_name, rc_caller_phone=@caller_phone, rc_referral=@referral, rc_referee=@referee, rc_unit=@unit, rc_summary=@summary output INSERTED.rc_idnt WHERE rc_idnt=@idnt END");

            return record;
        }

        public Screening SaveScreening(Screening screen) {
            return screen;
        }

        public Appointment SaveAppointment(Appointment appt) {
            return appt;
        }
    }
}
