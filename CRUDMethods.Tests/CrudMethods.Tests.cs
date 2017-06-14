using System;
using EpamTask.Models;
using EpamTask.Repositories.BaseCrudMethods;
using EpamTask.Repositories.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CRUDMethods.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetByIdCheck()
        {
           // int id = new int();
           // var mock = new Mock<CrudMethods<IdEntity>>();
           // mock.Setup(repo => repo.Get(id))
           //     .Returns(new IdEntity { Id = id });

           //var logic = new StudentRepository<IdEntity,CrudMethods<People>>(mock.Object);
           // var possitiveResult = logic.Get(id);
           // var negativeResult = logic.Get(new int());

           // Assert.AreEqual(id, possitiveResult.Id);
           // Assert.IsNull(negativeResult);

        }
    }
}
