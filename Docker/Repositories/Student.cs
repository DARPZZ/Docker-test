using MongoDB.Bson.Serialization.Attributes;

namespace Docker.Repositories
{
    public class Student
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
        public Student(Guid id, string name, string major)
        {
            this.Id = id;
            this.Name = name;
            this.Major = major;

        }

    }
}