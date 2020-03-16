namespace ChroniclesOfClevermist.Web.Areas.Administration.Controllers
{
    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
