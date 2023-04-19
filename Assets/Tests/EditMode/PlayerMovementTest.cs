using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/*
Script to test the Movement of the player. Created a function to test if the player has an invalid horizontal force (0).

Used Test Framework package v1.1.33. To execute/review the test cases you will need to add the Test Runner window (Window -> General -> Test Runner)
 */

namespace Tests.EditMode
{
    public class PlayerMovementTest
    {
        private static List<string> ReadTextFile(string path) //Function used to read a .txt file
        {
            StreamReader file = new StreamReader(path);

            List<string> testCasesList = new List<string>();

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                testCasesList.Add(line);
            }

            file.Close();

            return testCasesList;
        }
        
        public struct InvalidHorizontalForceTestCase  //Structure of InvalidHorizontalForce test case.
        {
            public int horizontalForce;

            public override string ToString()
            {
                return $"Horizontal Force: {horizontalForce.ToString()}"; //Name of the Test Case
            }
        }
        
        private static IEnumerable InvalidHorizontalForceTestCasesCreation() //Creation of the InvalidHorizontalForce test cases 
        {
            const string testCasesPath = "./Assets/Tests/EditMode/InvalidHorizontalForceCases.txt";
            
            List<string> testCasesInFile = ReadTextFile(testCasesPath); //Read the .txt file containing the test cases

            for (int i = 0; i < testCasesInFile.Count; i++) //For each line in the file, a new test case is created.
            {
                int arg1 = Int32.Parse(testCasesInFile[i]);
                
                yield return new InvalidHorizontalForceTestCase {horizontalForce = arg1};
            }
        }
        
        [Test]
        public void InvalidHorizontalForce([ValueSource(nameof(InvalidHorizontalForceTestCasesCreation))] InvalidHorizontalForceTestCase testCase) //Test cases generated in InvalidHorizontalForceTestCasesCreation function
        {
            const int invalidHorizontalForce = 0;
            
            Debug.Log("Set a Horizontal Force of " + testCase.horizontalForce + " --> Invalid Horizontal Force: " + invalidHorizontalForce);

            Assert.AreNotEqual(invalidHorizontalForce, testCase.horizontalForce); //Verification of the test
            
        }

    }
}
