using System.Diagnostics;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Docker.Repositories
{
  
    public interface IstudentRepository
    {
        void add(Student std);

        public Student get(Guid id);


    }
    public class StudentRepository : IstudentRepository
    {
        public StudentRepository(IOptions<D22RestDatabase> d22RestDatanase)
        {
            MongoClient mongoClient = new MongoClient(d22RestDatanase.Value.ConnectString);
            var mongoDatabase = mongoClient.GetDatabase(d22RestDatanase.Value.DatabaseName);
            _students = mongoDatabase.GetCollection<Student>(d22RestDatanase.Value.StudentsCollectionName);
                

        }
        private readonly IMongoCollection<Student> _students;


        public void add(Student std)
        {
            _students.InsertOne(std);

        }

        public Student? get(Guid id)
        {
            return _students.Find(std => std.Id == id).FirstOrDefault();
        }
    }

}
