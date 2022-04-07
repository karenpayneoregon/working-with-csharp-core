using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ApplicationDataLibrary;
using ApplicationDataLibrary.ComparerHelpers;
using ApplicationDataLibrary.Comparers;
using ApplicationDataLibrary.ExtensionMethods;
using ComparingObjectsUnitTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComparingObjectsUnitTest
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        
        /// <summary>
        /// Validate <see cref="FirstNameLastNameEqualityComparer"/> Distinct
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Distinct)]
        public void FirstNameLastName_Equality_Comparer()
        {
            var people = ReadPeopleFromFile;

            /*
             * Duplicate a person for ensuring we have something to work with
             */
            people.Add(people[0]);
            Assert.AreEqual(people.Count, 51);
            
            var distinct = people.DistinctFirstLastName() ; 
            
            Assert.IsTrue(distinct.Count() == 50);
        }

        /// <summary>
        /// Validate <see cref="FirstNameComparer"/> Distinct
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Distinct)]
        public void FirstName_Comparer()
        {
            var people = ReadPeopleFromFile;

            people[1].FirstName = people[2].FirstName;
            people[4].FirstName = people[2].FirstName;
            
            var distinct = people.DistinctFirstName(); 

            Assert.IsTrue(distinct.Count() == 48);

        }

        /// <summary>
        /// Perform test on first/last name for <seealso cref="Enumerable.SequenceEqual"/>
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SequenceEqual)]
        public void Sequence_Equals()
        {
            var people1 = FivePeople.ToArray();
            var people2 = FivePeople.ToArray();

            var results = people1.SequenceEqual(people2, new FirstNameLastNameEqualityComparer());
            Assert.IsTrue(results);

            people1[0].FirstName = "Mike";
            results = people1.SequenceEqual(people2, new FirstNameLastNameEqualityComparer());
            Assert.IsFalse(results);

        }

        /// <summary>
        /// Group by with comparer example
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Grouping)]
        public void GroupBy_Concert_Limit_Person_Id()
        {
            PersonIdentifierGroupComparer comparer = new PersonIdentifierGroupComparer();

            var concertGroups = Concerts.ConcertGroup(comparer);


            foreach (IGrouping<int, Concert> concertGroup in concertGroups)
            {
                Debug.WriteLine($"Concerts of person id {(comparer.IsInLimit(concertGroup.Key) ? "under 3" : "3 and over")} : ");
                foreach (Concert concert in concertGroup)
                {
                    Debug.WriteLine($"Number of concerts: {concert.ConcertCount}, in the year of {concert.Year} by singer {concert.PersonId}.");
                }
            }
        }

        [TestMethod]
        [TestTraits(Trait.GenericWrappers)]
        public void FirstName_Generic_Comparer()
        {
            /*
             * 50 people
             */
            var people = ReadPeopleFromFile;
            

            /*
             * Duplicate two people
             */
            people[1].FirstName = people[2].FirstName;
            people[4].FirstName = people[2].FirstName;


            var distinct = people.Distinct(Wrappers.FirstNamEqualityComparer);

            /*
             * Since two are duplicates from the original list we lose two
             */
            Assert.IsTrue(distinct.Count() == 48);

        }

        [TestMethod]
        [TestTraits(Trait.GenericWrappers)]
        public void LambdaEquals()
        {

            var people = TwoPersons;
            Assert.IsTrue(Wrappers.FirstNameLambda.Equals(people[0], people[1]));

            people[1].FirstName = "Mary";
            Assert.IsFalse(Wrappers.FirstNameLambda.Equals(people[0], people[1]));

        }
        /// <summary>
        /// Test override of equals operator for
        /// Id, First and Last name case insensitive
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.ObjectEqualsOverride)]
        public void IEquatable_Person()
        {

            var people = TwoPersons;
            
            people[0].Id = 1;
            people[1].Id = 1;
            Assert.IsTrue(people[0] == people[1]);

            people[0].Id = 2;
            Assert.IsTrue(people[0] != people[1]);

            people[0].Id = 1;
            people[0].FirstName = people[0].FirstName.ToLower();
            Assert.IsTrue(people[0] == people[1]);

        }


    }
}
