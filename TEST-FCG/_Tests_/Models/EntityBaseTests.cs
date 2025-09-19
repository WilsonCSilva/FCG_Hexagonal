using Domain.Models;

namespace TEST_FCG._Tests_.Models
{
    [TestClass]
    public class EntityBaseTest
    {
        [TestMethod]
        public void ConstructorSetsCreationDateToToday()
        {
            var entity = new EntityBase();
            Assert.AreEqual(DateTime.Now.Date, entity.DataCriacao);
        }

        [TestMethod]
        public void NameDefaultsToEmptyString()
        {
            var entity = new EntityBase();
            Assert.AreEqual(string.Empty, entity.Nome);
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            var entity = new EntityBase();
            entity.Nome = "TestName";
            Assert.AreEqual("TestName", entity.Nome);
        }

        [TestMethod]
        public void CanSetAndGetId()
        {
            var entity = new EntityBase();
            entity.Id = 123;
            Assert.AreEqual(123, entity.Id);
        }

        [TestMethod]
        public void CanSetAndGetCreationDate()
        {
            var entity = new EntityBase();
            var date = new DateTime(2022, 5, 1);
            entity.DataCriacao = date;
            Assert.AreEqual(date, entity.DataCriacao);
        }

        //[TestMethod]  
        //public void CanSetAndGetEmail()  
        //{  
        //    var entity = new EntityBase();  
        //    entity.Email = "test@email.com";  
        //    Assert.AreEqual("test@email.com", entity.Email);  
        //}  

        //[TestMethod]  
        //public void EmailCanBeNull()  
        //{  
        //    var entity = new EntityBase();  
        //    entity.Email = null;  
        //    Assert.IsNull(entity.Email);  
        //}  
    }
}
