
Short documentation

	-Solution is build on .NET Framework 4.7.1, ASP.NET MVC 5 application
	-Visual Studio - 2017 v.15
	-Entity Framework - 6.2
	-Current used MS SQL Server is v.17
	
Additional libs
	-Autofac 4.0.1 - Autofac is an IoC container for Microsoft .NET. It manages the dependencies between classes so that applications stay easy to change as they grow in size and complexity.
	-Bootstrap 3.0 - Responsive grid system, extensive prebuilt components, and powerful plugins built on jQuery.
	-jQuery 3.3.1 - dependency of Bootstrap and form validator on client side.
	
Additional info
	-Web application project is separate in multiple projects for team development. There are several layers like data/ORM/, core, models and frond-end.
	
Web App info
	-Everyone can register a car dealer. There is no check if current dealer name is used /unique/.
	-There is a dealer list where can filter by name. Correct filter input is text between of 3 and 20 charcter length. Filter field should match part or full name of a car.
	-If you wish to add a car. There should be any dealer added. All properties of a car are required!
	-By same logic car list has filter...where you can search by part or full match of car description or just by car dealer or mix by both checks.