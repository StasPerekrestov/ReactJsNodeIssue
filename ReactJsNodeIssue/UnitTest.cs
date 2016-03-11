using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using React;

namespace ReactJsNodeIssue {
	[TestClass]
	public class UnitTest {
		private const string TestJsxCode = 
			"for (let i = 0; i < items['hazards'].length(); i++) {"+
					"fq.then(function(cb) { "+
						"fillHazard(items['hazards'].items(i), cb);"+
					"}).then(fillAvailableClasses);"+
			"}";

		private static void Initialize() {
			Initializer.Initialize(registration => registration.AsSingleton());
			var container = React.AssemblyRegistration.Container;

			container.Register<ICache, NullCache>();
			container.Register<IFileSystem, SimpleFileSystem>();
		}

		[TestInitialize]
		public void SetUp() {
			Initialize();
		}

		[TestMethod]
		public void Test() {
			IBabel babel = ReactEnvironment.Current.Babel;
			string transformedJs = babel.Transform(TestJsxCode);
			Assert.IsNotNull(transformedJs);
		}
	}
}
