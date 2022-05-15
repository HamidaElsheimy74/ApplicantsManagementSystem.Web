
# Applicants Management System

A brief description of what this project does and who it's for


## Acknowledgements

 - This project to is made for managing applican's different operations.
 - After oppening the home page you can navigate to Applicants List to see all the system applicants.
 - You can add a new appliacnt to the system and see it on the list after.
 - You can Edit an existing applicant.
 - You can't add the two applicants with the same (Email & Name & family name).
 - You can delete applicant of the system.
 - You can see applicant details.  
   

## API Reference

#### Get all applicants data from database

```
  https://localhost:44382/api/Applicant/GetAll
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
 No Parameters is needed

#### Get item

```
  https://localhost:44382/api/Applicant/GetById?Id={id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |

#### Delete item

```
  https://localhost:44382/api/Applicant/Delete?Id={id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item to fetch |

#### Add New item

```
  https://localhost:44382/api/Applicant/Add
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item |
| `Name`    | `string` | **Required & >=5 char**. Name of item|
| `FamilyName`| `string` | **Required & >=5 Char**.Family Name of item |
| `Address`   | `string` | **Required & >=10 char**. Address of item |
| `EmailAddress`  | `string` | **Required**. Email of item |
| `CountryOfOrigin` | `string` | **Required**. Country of origin of item  |
| `Age`   | `int` | **Required & 20>age<60**. Age of item  |
`Hired`   | `bool` | **not Required**. if item is hired or not  |

#### Edit item
```
  https://localhost:44382/api/Applicant/Update
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required**. Id of item |
| `Name`    | `string` | **Required & >=5 char**. Name of item|
| `FamilyName`| `string` | **Required & >=5 Char**.Family Name of item |
| `Address`   | `string` | **Required & >=10 char**. Address of item |
| `EmailAddress`  | `string` | **Required**. Email of item |
| `CountryOfOrigin` | `string` | **Required**. Country of origin of item  |
| `Age`   | `int` | **Required & 20>age<60**. Age of item  |
`Hired`   | `bool` | **not Required**. if item is hired or not  |

#### returns if CountryOfOrigin is valid or not

```
 https://restcountries.com/v2/name/{countyName}?fullText=true
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
 | `name`      | `string` | **Required**. name of the Country to fetch |




## 🔗 Links
[![Github](https://github.com/HamidaElsheimy74/ApplicantsManagementSystem.Web)

## Deployment
#Site url
https://localhost:44370/Applicant
## Tech Stack

**Client:** ASP.net core Razor

**Server:**.net core, c#, Entity framework core.

