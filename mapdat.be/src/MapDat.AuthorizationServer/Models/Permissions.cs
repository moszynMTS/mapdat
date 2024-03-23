namespace AuthorizationServer.Models
{
    public enum Permissions : short
    {
        NotSet = 0, //error condition

        AccessWeb = 10,
        AccessClientWeb = 11,

        UserRead = 40,
        UserChange = 41,

        RoleRead = 50,
        RoleChange = 51,

        ClientRead = 60,
        ClientChange = 61,

        ResourceRead = 70,
        ResourceChange = 71,

        //CreateUserAccount = 100,
        //UpdateUserAccount = 101,
        //DeleteUserAccount = 102,


        // Projects
        ViewAllProjects = 200,
        ViewAssignedProjects = 201,
        ViewProjectDetails = 202,
        ViewProjectTemplates = 203,

        CreateProject = 210,
        UpdateProject = 211,
        AssignUsersToProject = 213,
        DeleteProjects = 214,


        CreateProjectTemplate = 220,

        // Areas
        ViewAreas = 300,

        CreateArea = 310,
        UpdateArea = 311,
        DeleteArea = 312,

        // Sections
        ViewSections = 400,

        CreateSection = 410,
        UpdateSection = 411,
        DeleteSection = 412,

        // Labeles
        ViewLabels = 500,

        CreateLabel = 510,
        UpdateLabel = 511,
        DeleteLabel = 512,
        CreateChildLabelBeforeParentIsComplete = 513,

        PrintLabel = 520,
        AddLabelCode = 521,
        AddLabelPhoto = 522,
        //RestoreLabels = 533,

        // Codes
        ViewCodes = 600,

        CreateCode = 610,
        UpdateCode = 611,
        DeleteCode = 612,

        // Customers
        ViewCustomers = 700,

        CreateCustomer = 710,
        UpdateCustomer = 711,
        DeleteCustomer = 712,

        // Identity
        IdentityUsersRead = 32000,
        IdentityUsersUpdate = 32001,
        IdentityRolesRead = 32000,
        IdentityRolesUpdate = 32001,
        IdentityClientsRead = 32100,
        IdentityClientsUpdate = 32101,
        //Cargo
        ViewCargo = 800,
        CreateCargo = 810,
        UpdateCargo = 811,
        DeleteCargo = 812,

        //Transport List
        ViewTransportList = 900,
        UpdateTransportList = 901,
        DeleteTransportList = 902,
        AddPhotoToTransportList = 903,
        CreateTransportList = 904,


        //Task 
        CreateTask = 1000,

        //Page view history
        PageViewHistory = 1100,

        AccessAll = short.MaxValue,
    }
}