# Upload-and-display-image-in-MVC\
There is a very simple way to store images in project's folder and save folder path in database from MVC application.\
##Step 1: First we create a class named Patient in model folder.\
 public class Patient
   {
        public int Id { get; set; }
      public string Name
      {
         get; set;
      }

      public string Password { get; set; }
      public string About { get; set; }

      public string hobby { get; set; }

      public string gender { get; set; }

      public string country { get; set; }
      public string SCountry { get; set; }
        [DataType(DataType.Date)]
        public DateTime Bdate { get; set; }
        public bool isActive { get; set; }
      public string profilePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase profile { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
    }
    ##Step 2: Run command  for migration.
      ### Enable-migrations [for first time]
      ###add-migration image
      ###update-database
    ##Step 3: Create  Action Method in Controller class .
      [HttpGet]
      public ActionResult Create() {

         return View();
      }
      ##Step 3: Create  View for input record .
      @model WebApplication1.Models.Patient
          @{
              ViewBag.Title = "Create";
          }

        <h2>Create</h2>
@ViewBag.mesaage
@using (Html.BeginForm("Create", "Patients", FormMethod.Post, new { enctype = "multipart/form-data" }))
{
    <div>
        @Html.Label("lblname", "Name")
        @Html.TextBoxFor(m => m.Name, "", new { @placeholder = "Enter Name", @class = "form-control" })
    </div>

    <div>
        @Html.Label("lblpassword", "Password")
        @Html.PasswordFor(m => m.Password, new { @class = "form-control", @placeholder = "Enter Password" })
    </div>


    <div>
        @Html.Label("lblAbout", "About")
        @Html.TextAreaFor(m => m.About, new { @class = "form-control", @placeholder = "Write yourself" })
    </div>

    @*<div>
           @Html.Label("lblhobby", "Your Hobby")
           <div>
              <span>Reading</span> @Html.CheckBox("hobyylist", "Reading")
                 <span>Writing</span> @Html.CheckBox("hobyylist", "Writing")
                 <span>Singing</span> @Html.CheckBox("hobyylist", "Singing")

           </div>
        </div>*@
    <div class="form-group row">
        @Html.Label("lblgender", "Your Gender", new { @class = "col-md-4" })
        <div class="row col-md-8    ">
            <span>Male</span>  @Html.RadioButton("chkgender", "Male")
            <span>Female</span>  @Html.RadioButton("chkgender", "Female")

        </div>
    </div>
    <div>
        @Html.Label("lblbcountry", "Country")
        @{
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Bangladesh",
                Value = "Bangladesh"
            });
            list.Add(new SelectListItem { Text = "America", Value = "America" });
            list.Add(new SelectListItem { Text = "Canada", Value = "Canada" });
        }
        @Html.DropDownListFor(m => m.country, list, "Select Country", new { @class = "form-control" })
    </div>
    <div>
        @Html.Label("lblbactive", "Active")
        @Html.EditorFor(m => m.isActive, new { @class = "form-control", @placeholder = "Enter Birthdate" })
    </div>
    <div>
        @Html.Label("lblbdate", "Birthday")
        @Html.EditorFor(m => m.Bdate, new { @class = "form-control", @placeholder = "Enter Birthdate" })
    </div>

    <div>
        @Html.Label("lblprofile", "Upload Image")
        <input type="file" name="Image" />

    </div>
    <div>
        @Html.Label("lblprofile", "Upload profile")
        <input type="file" name="profile" />

    </div>
    <div>
        <input type="submit" value="Save" />
    </div>
}

 ##Step 4: Create  Action method for Post/save data in Controller .
   [HttpPost]
        public ActionResult Create(Patient patient)
        {
            string folderpath =Path.Combine( Server.MapPath("~/"),"PatientProfile");
            string fname = Path.GetFileName(patient.profile.FileName);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            if(patient.profile !=null)
            {
                patient.profile.SaveAs(Path.Combine( folderpath, fname));
            }

            patient.profilePath = "~/PatientProfile/" + fname;

            //image upload
            if (patient.Image!=null)
            {
                string imagePath = Path.Combine(Server.MapPath("~/"), "PatientImage");
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }
                string ext = Path.GetExtension(patient.Image.FileName);
                string imgFileName = Path.GetFileName(patient.Image.FileName).ToString();
                if(ext.ToLower()==".jpg"|| ext.ToLower() == ".jpeg" || ext.ToLower() == ".png")
                {
                    patient.Image.SaveAs(Path.Combine(imagePath, imgFileName));
                    patient.ImagePath = "~/PatientImage/" + imgFileName;
                }
                else
                {
                    ViewBag.mesaage = "please provide jpg/jpeg/png type picture";
                    return View();
                }
            }
            db.Patients.Add(patient);
           if( db.SaveChanges()>0)
            {
                ViewBag.mesaage = "Save success";
                return RedirectToAction("Index");
            }
           else
            {
                ViewBag.mesaage = "Save failed";
            }
            return View();
        }

 ##Step 5: Create  Action method for display data with Image in Controller .
   dbHospital db=new dbHospital();
        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.OrderBy(p=>p.Name).Where(p=>p.isActive).ToList());
        }
        
 ##Step 6: Create  View for display data with Image  .
      @model IEnumerable<WebApplication1.Models.Patient>
@{
    ViewBag.Title = "Display Patient Records";
}

<h2>All Patient</h2>
@Html.ActionLink("Add", "Create", "Patients")
<div class="row">
    @foreach (var item in Model)
    {

        <div class="card col-md-4" style="width: 20rem;">
            @if(@item.ImagePath!=null)
            {
            <img class="card-img-top" src="@Url.Content(item.ImagePath)" alt="Card image cap" height="286" width="180">
            }
            
            <div class="card-body">
                <h5 class="card-title">@item.Name</h5>
                <p class="card-text">@item.About</p>
                
                @Html.ActionLink("Details", "Details", "Patients", new { @id = item.Id }, new {@class="btn btn-primary"})
            </div>
        </div>
    }
</div>



    
      
