1 - The frontend project is TaskMgtClient.

2 - The backend project is TaskManagementBackend.

3 - The backend project must be ran before the frontend project is ran as api calls are made from the frontend to the backend.

4 - To run the backend project, please open the solution in visual studio 2019 or later, if the project does not build, please clean the solution, and rebuild all. Then launch the project again.

5 - Once the backend project is ran, copy the base url without the swagger text and api to the back like this "https://localhost:44345/api/", we will add this to the base url of the frontend project. i will explain this in step 7.

6 - Now that the backend is running, open the frontend project and do "npm install" to restore node packages, if you get an error relating to dependicies being out of date, please use this command instead to force npm to use this "npm install --legacy-peer-deps" legacy versions used in the project.

7 - Navigate to the appsettings.json in the root folder of the frontend project (TaskMgtClient). if the baseUrl defined inside this json file is different from the one
you have copied in step 5. then replace it with the one you have copied.
