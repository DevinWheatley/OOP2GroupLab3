using Assignment3;
using System.Runtime.Intrinsics.X86;

namespace Assignment3.Tests
{
    public class SerializationTests
    {
        private ILinkedListADT users;
        private readonly string testFileName = "test_users.bin";

        [SetUp]
        public void Setup()
        {
            // Uncomment the following line
            this.users = new SLL();

            users.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            users.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            users.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            users.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));
        }

        [TearDown]
        public void TearDown()
        {
            this.users.Clear();
        }

        /// <summary>
        /// Tests the object was serialized.
        /// </summary>
        [Test]
        public void TestSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            Assert.IsTrue(File.Exists(testFileName));
        }

        /// <summary>
        /// Tests the object was deserialized.
        /// </summary>
        [Test]
        public void TestDeSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            ILinkedListADT deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);
            
            Assert.IsTrue(users.Count() == deserializedUsers.Count());
            
            for (int i = 0; i < users.Count(); i++)
            {
                User expected = users.GetValue(i);
                User actual = deserializedUsers.GetValue(i);

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Password, actual.Password);
            }
        }

        /// <summary>
        /// Tests the list is empty
        /// </summary>
        [Test]
        public void TestIsEmpty()
        {
            users.Clear(); 
            Assert.IsTrue(users.IsEmpty());
            users.AddLast(new User(5, "Bob", "bob@gmail.com", "test"));
            Assert.IsFalse(users.IsEmpty());
        }

        /// <summary>
        /// Tests that Clear() properly clears all Nodes from the list
        /// </summary>
        [Test]
        public void TestClear()
        {
            users.AddLast(new User(5, "Bob", "bob@gmail.com", "test"));
            users.Clear();
            Assert.IsTrue(users.IsEmpty());
        }

        /// <summary>
        /// Tests that the New Node adds to the end of the list
        /// </summary>
        [Test]
        public void TestAddLast()
        {
            users.AddLast(new User(5, "Bob", "bob@gmail.com", "test"));
            Assert.That(users.Count(), Is.EqualTo(5));
            Assert.That(users.GetValue(users.Count() - 1).Name, Is.EqualTo("Bob"));
        }

        /// <summary>
        /// Tests that the New Node adds to the beginning of the list
        /// </summary>
        [Test]
        public void TestAddFirst()
        {
            users.AddFirst(new User(5, "Bob", "bob@gmail.com", "test"));
            Assert.That(users.Count(), Is.EqualTo(5));
            Assert.That(users.GetValue(0).Name, Is.EqualTo("Bob"));
        }

        /// <summary>
        /// Tests that the New Node is properly added at the chosen index
        /// </summary>
        [Test]
        public void TestAdd()
        {
            users.Add(new User(5, "Bob", "bob@gmail.com", "test"), 2);
            Assert.That(users.Count(), Is.EqualTo(5));
            Assert.That(users.GetValue(2).Name, Is.EqualTo("Bob"));
        }

        /// <summary>
        /// Tests that a Node is properly replaced by the New Node
        /// </summary>
        [Test]
        public void TestReplace()
        {
            users.Replace(new User(5, "Bob", "bob@gmail.com", "test"), 1);
            Assert.That(users.GetValue(1).Name, Is.EqualTo("Bob"));
        }

        /// <summary>
        /// Tests that Count() returns the proper amount of Nodes in the List
        /// </summary>
        [Test]
        public void TestCount()
        {
            Assert.That(users.Count(), Is.EqualTo(4));
        }

        /// <summary>
        /// Tests that the first Node is removed and the other Nodes are shifted
        /// </summary>
        [Test]
        public void TestRemoveFirst()
        {
            users.RemoveFirst();
            Assert.That(users.Count(), Is.EqualTo(3));
            Assert.That(users.GetValue(0).Name, Is.EqualTo("Joe Schmoe"));
        }

        /// <summary>
        /// Tests that the last Node is removed and the other Nodes are shifted
        /// </summary>
        [Test]
        public void TestRemoveLast()
        {
            users.RemoveLast();
            Assert.That(users.Count(), Is.EqualTo(3));
            Assert.That(users.GetValue(2).Name, Is.EqualTo("Colonel Sanders"));
        }

        /// <summary>
        /// Tests that the Node at given index is removed and the other Nodes are shifted
        /// </summary>
        [Test]
        public void TestRemove()
        {
            users.Remove(1);
            Assert.That(users.Count(), Is.EqualTo(3));
            Assert.That(users.GetValue(1).Name, Is.EqualTo("Colonel Sanders"));
        }

        /// <summary>
        /// Tests that GetValue() is indexing the correct Nodes
        /// </summary>
        [Test]
        public void TestGetValue()
        {
            Assert.That(users.GetValue(0).Name, Is.EqualTo("Joe Blow"));
            Assert.That(users.GetValue(1).Id, Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that IndexOf() is returning the proper first index of the argumnent's User
        /// </summary>
        [Test]
        public void TestIndexOf()
        {
            Assert.That(users.IndexOf(new User(1, "Joe Blow", "jblow@gmail.com", "password")), Is.EqualTo(0));
            Assert.That(users.IndexOf(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef")), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that Contains() is properly checking over the list and returning the correct boolean for if a User exists in the list or not
        /// </summary>
        [Test]
        public void TestContains()
        {
            Assert.IsTrue(users.Contains(new User(1, "Joe Blow", "jblow@gmail.com", "password")));
            Assert.IsFalse(users.Contains(new User(5, "Bob", "bob@gmail.com", "test")));
        }

        /// <summary>
        /// Tests that all Nodes are properly reversed in order
        /// </summary>
        [Test]
        public void TestReverse()
        {
            users.Reverse();
            Assert.That(users.GetValue(0).Name, Is.EqualTo("Ronald McDonald"));
            Assert.That(users.GetValue(1).Name, Is.EqualTo("Colonel Sanders"));
            Assert.That(users.GetValue(2).Name, Is.EqualTo("Joe Schmoe"));
            Assert.That(users.GetValue(3).Name, Is.EqualTo("Joe Blow"));
        }
    }
}