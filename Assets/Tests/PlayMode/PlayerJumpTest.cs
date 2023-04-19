using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

/*
Script to test the Jump of the player. Created a function to test if the player reaches the indicated maximum y position depending on the vertical force applied

Used Test Framework package v1.1.33. To execute/review the test cases you will need to add the Test Runner window (Window -> General -> Test Runner)
 */

namespace Tests.PlayMode
{
    public class PlayerJumpTest
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
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Scenes/DemoScene");
        }
        
        public struct MaximumYPositionTestCase  //Structure of MaximumYPosition test case.
        {
            public int verticalForce;
            public int expectedMaximumYPosition;
            
            public override string ToString()
            {
                return $"Vertical Force: {verticalForce.ToString()}. Expected maximum Y position: {expectedMaximumYPosition.ToString()}"; //Name of the Test Case
            }
        }
        
        private static IEnumerable MaximumYPositionTestCasesCreation() //Creation of the MaximumYPosition test cases 
        {
            const string testCasesPath = "./Assets/Tests/PlayMode/MaximumYPositionCases.txt";
            
            List<string> testCasesInFile = ReadTextFile(testCasesPath); //Read the .txt file containing the test cases

            for (int i = 0; i < testCasesInFile.Count; i++) //For each line in the file, a new test case is created. Arguments are obtained by splitting the line using a comma character
            {
                int arg1 = Int32.Parse(testCasesInFile[i].Split(";")[0]); //Before semicolon
                int arg2 = Int32.Parse(testCasesInFile[i].Split(";")[1]); //After semicolon

                yield return new MaximumYPositionTestCase { verticalForce = arg1, expectedMaximumYPosition = arg2};
            }
        }
        
        [UnityTest]
        public IEnumerator MaximumYPosition([ValueSource(nameof(MaximumYPositionTestCasesCreation))] MaximumYPositionTestCase testCase) //Test cases generated in MaximumYPositionTestCasesCreation function
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            PlayerMovement player = gameObject.GetComponent<PlayerMovement>(); //Player object obtained from the Scene by the Tag
            
            Debug.Log(("Original Horizontal Force: " + player.horizontalForce + " & Vertical Force: " + player.verticalForce));

            player.verticalForce = testCase.verticalForce;
            
            int maximumYPosition = (int)player.Jump(); //Review PlayerMovement.cs in Scripts folder, Jump function

            Debug.Log("Applied a Vertical Force of " + testCase.verticalForce + " --> Expected maximum Y position: " + testCase.expectedMaximumYPosition + ". Maximum Y position reached: " + maximumYPosition);
            
            Assert.AreEqual(maximumYPosition, testCase.expectedMaximumYPosition); //Verification of the test
            
            yield return null;
        }

    }
}
