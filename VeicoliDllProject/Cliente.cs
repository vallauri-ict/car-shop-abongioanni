using System;
using System.Collections.Generic;
using System.Text;

namespace VenditaVeicoliDllProject {
    public class Cliente {

        #region PROPERTIES
        private string gender;
        private string nameTitle;
        private string nameFirst;
        private string nameLast;
        private string locationCity;
        private string locationState;
        private string locationPostcode;
        private double locationCoordinatesLatitude;
        private double locationCoordinatesLongitude;
        private string locationTimezoneOffset;
        private string locationTimezonedescription;
        private string email;
        private string loginUuid;
        private string loginUsername;
        private string loginPassword;
        private string loginSalt;
        private string loginMd5;
        private string loginSha1;
        private string loginSha256;
        private DateTime dobDate;
        private int dobAge;
        private DateTime registeredDate;
        private int registeredAge;
        private string phone;
        private string cell;
        private string idName;
        private string idValue;
        private string pictureLarge;
        private string pictureMedium;
        private string pictureThumbnail;
        private string nat;

        public string Gender { get => this.gender; set => this.gender = value.ToUpper() == "MALE" || value.ToUpper() == "FEMALE" ? value : throw new Exception(); }
        public string NameTitle { get => this.nameTitle; set => this.nameTitle = value; }
        public string NameFirst { get => this.nameFirst; set => this.nameFirst = value; }
        public string NameLast { get => this.nameLast; set => this.nameLast = value; }
        public string LocationStreetName { get; set; }
        public string LocationCity { get => this.locationCity; set => this.locationCity = value; }
        public string LocationState { get => this.locationState; set => this.locationState = value; }
        public string LocationPostcode { get => this.locationPostcode; set => this.locationPostcode = value; }
        public double LocationCoordinatesLatitude { get => this.locationCoordinatesLatitude; set => this.locationCoordinatesLatitude = value; }
        public double LocationCoordinatesLongitude { get => this.locationCoordinatesLongitude; set => this.locationCoordinatesLongitude = value; }
        public string LocationTimezoneOffset { get => this.locationTimezoneOffset; set => this.locationTimezoneOffset = value; }
        public string LocationTimezoneDescription { get => this.locationTimezonedescription; set => this.locationTimezonedescription = value; }
        public string Email { get => this.email; set => this.email = value; }
        public string LoginUuid { get => this.loginUuid; set => this.loginUuid = value; }
        public string LoginUsername { get => this.loginUsername; set => this.loginUsername = value; }
        public string LoginPassword { get => this.loginPassword; set => this.loginPassword = value; }
        public string LoginSalt { get => this.loginSalt; set => this.loginSalt = value; }
        public string LoginMd5 { get => this.loginMd5; set => this.loginMd5 = value; }
        public string LoginSha1 { get => this.loginSha1; set => this.loginSha1 = value; }
        public string LoginSha256 { get => this.loginSha256; set => this.loginSha256 = value; }
        public DateTime DobDate { get => this.dobDate; set => this.dobDate = value; }
        public int DobAge { get => this.dobAge; set => this.dobAge = value; }
        public DateTime RegisteredDate { get => this.registeredDate; set => this.registeredDate = value; }
        public int RegisteredAge { get => this.registeredAge; set => this.registeredAge = value; }
        public string Phone { get => this.phone; set => this.phone = value; }
        public string Cell { get => this.cell; set => this.cell = value; }
        public string IdName { get => this.idName; set => this.idName = value; }
        public string IdValue { get => this.idValue; set => this.idValue = value; }
        public string PictureLarge { get => this.pictureLarge; set => this.pictureLarge = value; }
        public string PictureMedium { get => this.pictureMedium; set => this.pictureMedium = value; }
        public string PictureThumbnail { get => this.pictureThumbnail; set => this.pictureThumbnail = value; }
        public string Nat { get => this.nat; set => this.nat = value; }
        public Dictionary<string, string> Name { get; set; }
        public string LocationStreetNumber { get; set; }
        public Dictionary<string, object> Location { get; set; }
        public Dictionary<string, string> Login { get; set; }
        public Dictionary<string, object> Dob { get; set; }
        public Dictionary<string, object> Registered { get; set; }
        public Dictionary<string, string> Id { get; set; }
        public Dictionary<string, string> Picture { get; set; }
        #endregion

        public Cliente(Newtonsoft.Json.Linq.JToken u)
        {

            this.Gender = u["gender"].ToString();

            this.NameTitle = u.SelectToken("$.name.title").ToString();
            this.NameFirst = u.SelectToken("$.name.first").ToString();
            this.NameLast = u.SelectToken("$.name.last").ToString();
            this.Name = new Dictionary<string, string>()
            {
                { "title", this.NameTitle } ,
                { "first",this.NameFirst},
                { "last",this.NameLast},
            };

            this.LocationStreetNumber = u.SelectToken("$.location.street.number").ToString();
            this.LocationStreetName = u.SelectToken("$.location.street.name").ToString();
            this.LocationCity = u.SelectToken("$.location.city").ToString();
            this.LocationState = u.SelectToken("$.location.state").ToString();
            this.LocationPostcode = u.SelectToken("$.location.postcode").ToString();
            this.LocationCoordinatesLatitude = Convert.ToDouble(u.SelectToken("$.location.coordinates.latitude"));
            this.LocationCoordinatesLongitude = Convert.ToDouble(u.SelectToken("$.location.coordinates.longitude"));
            this.LocationTimezoneOffset = u.SelectToken("$.location.timezone.offset").ToString();
            this.LocationTimezoneDescription = u.SelectToken("$.location.timezone.description").ToString();
            this.Location = new Dictionary<string, object>()
            {
                {"street",new Dictionary<string,string>(){ { "number", this.LocationStreetNumber },{"name",this.LocationStreetName } } },
                {"city",this.LocationCity },
                {"state",this.LocationState },
                {"postcode",this.LocationPostcode },
                {"coordinates",new Dictionary<string,double>(){{"latitude", this.LocationCoordinatesLatitude },{"longitude", this.LocationCoordinatesLongitude } } },
                {"timezone",new Dictionary<string,string>(){{"offset", this.LocationTimezoneOffset },{ "description", this.LocationTimezoneDescription } } },
            };

            this.Email = u.SelectToken("$.email").ToString();

            this.LoginUuid = u.SelectToken("$.login.uuid").ToString();
            this.LoginUsername = u.SelectToken("$.login.username").ToString();
            this.LoginPassword = u.SelectToken("$.login.password").ToString();
            this.LoginSalt = u.SelectToken("$.login.salt").ToString();
            this.LoginMd5 = u.SelectToken("$.login.md5").ToString();
            this.LoginSha1 = u.SelectToken("$.login.sha1").ToString();
            this.LoginSha256 = u.SelectToken("$.login.sha256").ToString();
            this.Login = new Dictionary<string, string>()
            {
                { "uuid", this.LoginUuid } ,
                { "username",this.LoginUsername},
                { "password",this.LoginPassword},
                { "salt",this.LoginSalt},
                { "md5",this.LoginMd5},
                { "sha1",this.LoginSha1},
                { "sha256",this.LoginSha256},
            };

            this.DobDate = Convert.ToDateTime(u.SelectToken("$.dob.date"));
            this.DobAge = Convert.ToInt32(u.SelectToken("$.dob.age"));
            this.Dob = new Dictionary<string, object>()
            {
                { "date", this.DobDate } ,
                { "age",this.DobAge},
            };

            this.RegisteredDate = Convert.ToDateTime(u.SelectToken("$.registered.date"));
            this.RegisteredAge = Convert.ToInt32(u.SelectToken("$.registered.age"));
            this.Registered = new Dictionary<string, object>()
            {
                { "date", this.RegisteredDate } ,
                { "age",this.RegisteredAge},
            };

            this.Phone = u.SelectToken("$.phone").ToString();

            this.Cell = u.SelectToken("$.cell").ToString();

            this.IdName = u.SelectToken("$.id.name").ToString();
            this.IdValue = u.SelectToken("$.id.value").ToString();
            this.Id = new Dictionary<string, string>()
            {
                { "name", this.IdName } ,
                { "value",this.IdValue},
            };

            this.PictureLarge = u.SelectToken("$.picture.large").ToString();
            this.PictureMedium = u.SelectToken("$.picture.medium").ToString();
            this.PictureThumbnail = u.SelectToken("$.picture.thumbnail").ToString();
            this.Picture = new Dictionary<string, string>()
            {
                { "large", this.PictureLarge } ,
                { "medium",this.PictureMedium},
                { "thumbnail",this.PictureThumbnail},
            };
            this.Nat = u.SelectToken("$.nat").ToString();

        }

        public string NameComplete()
        {
            return $"{this.nameTitle} {this.NameFirst} {this.NameLast}";
        }

        public string Address()
        {
            return $"{this.LocationStreetNumber} {this.LocationStreetName}, {this.LocationCity} - {this.LocationState}";
        }
    }

}
