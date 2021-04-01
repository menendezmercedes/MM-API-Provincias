# MM-API-Provincias
This is an API to get the latitude and longitude of different Argentinian provinces.

For change the default user, you must edit the appsetting.json that is in the folder bin-> netcoreapp2.1.
The api generates the correct sql connection string using the information that is in the DataBase section of the appsetting.json.
Take into account that the Catalog make references to the name of the DataBase.

## Test Project

Make sure that the appsetting.json is include in ..\API_Provinces.MTest\bin\Debug\netcoreapp2.1
The api generates the correct sql connection string using the information that is in the DataBase section of the appsetting.json.
Take into account that the Catalog make references to the name of the DataBase.

## TO-DO
Requires the Authentication token generated in the login ENDPOINT.

## General Comments
I decided that it was not necessary any validation of the province names, because the api that gets the latitude and logintud have a lot of validations, and considerations. They have differents way to name a province, so I think it was not necessary to do any kind of validation of that.

