using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SqlExceptionTalk.Tests
{
    public class XunitTestPriority
    {
        public class TestPriorityAttribute : Attribute
        {
            public int Priority { get; }

            public TestPriorityAttribute(int priority)
            {
                Priority = priority;
            }
        }

        public class TestCollectionOrderer : ITestCaseOrderer
        {
            public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
            {
                var sortedMethods = new SortedDictionary<int, TTestCase>();

                foreach (TTestCase testCase in testCases)
                {
                    IAttributeInfo attribute = testCase.TestMethod.Method.
                        GetCustomAttributes((typeof(TestPriorityAttribute)
                            .AssemblyQualifiedName)).FirstOrDefault();

                    var priority = attribute?.GetNamedArgument<int>("Priority");
                    if (priority != null)
                        sortedMethods.Add((int)priority, testCase);
                }

                return sortedMethods.Values;
            }
        }
    }
}
