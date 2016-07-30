using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal;
using Portal.Controllers;
using Portal.Services;
using Portal.Tests.Repositories;
using Portal.Entities;

namespace Portal.Tests.Controllers
{
    [TestClass]
    public class StoreControllerTest
    {
        private StoreController _controller;
        private StoreService _service;
        private TestProductRepository _products;

        #region on / off
        [TestInitialize()]
        public void Startup()
        {
            _products = new TestProductRepository(100);
            _service = new StoreService(_products);
            _controller = new StoreController(_service);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _products = null;
            _service = null;
            _controller = null; 
        }
        #endregion on / off
        
        [TestMethod]
        public void Index()
        {
            // Arrange
            // Act
            var result1 = _controller.Index(null).Result as ViewResult;
            //// Assert
            Assert.AreEqual(((IEnumerable<Product>)result1.ViewData["products"]).Count(), 100);

            var result2 = _controller.Index(54).Result as ViewResult;            
            Assert.AreEqual(((Product)result2.Model).Id, _products.GetByIdAsync(54).Result.Id);
        }
    }
}
