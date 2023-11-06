using System.Diagnostics;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Docker.Repositories
{
    public class StudentRepositoryKlasse
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
                try
                {
                    _students.InsertOne(new Student(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2"), "wdawdwadwwdawdwadwadwadwdadwadwada", "nej tak"));
                }
                catch (Exception ex) { Debug.WriteLine(ex); }

            }
            private readonly IMongoCollection<Student> _students;

          
            public void add(Student std)
            {
                _students.InsertOne(std);

            }

            public Student? get(Guid id)
            {
                return _students.Find(std => std.Id == id).FirstOrDefault() ;
            }
        }

    }
}

