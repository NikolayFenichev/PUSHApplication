using Firebase.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirebaseService.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FirebaseController : Controller
    {
        private IFirebaseService _firebaseService;

        public FirebaseController(IFirebaseService service)
        {
            _firebaseService = service;
        }

        [HttpGet]
        public void Start()
        {
            _firebaseService.Start();
        }
    }
}
