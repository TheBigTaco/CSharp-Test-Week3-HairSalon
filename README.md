# Hair Salon
##### by Adam Titus

### Description
This is a mockup of a website that a hair salon could use. It includes the ability to add stylists, and customers to those stylists.
### Installation Instructions
* Copy the url for this repository
* Open Bash or something similar
* In Bash, type in `cd desktop` and hit enter
* then type `git clone` plus the url you copied and hit enter
* then type `cd CSharp-Test-Week3-HairSalon/HairSalon.Solution/HairSalon` and hit enter
* Now type `dotnet restore` and wait for everything to load
* Now open MAMP
* Click the Start Servers button
* Now go back to Bash and type in `MySql -uroot -proot`
* Once your command line changes to `MySql>` type in `CREATE DATABASE adam_titus`
* Now type in the following commands
> * USE adam_titus
> * SELECT DATABASE();
> * CREATE TABLE stylists(id serial PRIMARY KEY, name VARCHAR(255));
> * CREATE TABLE customers(id serial PRIMARY KEY, name VARCHAR(255), stylist_id int);
> * \q
* Now type in dotnet run
* Open to your web browser and navigate too `http://localhost:5000`

### Technology Needed
* MAMP
* MySql
* .Net
* Bash
* Web Browser with Internet

### Specs
|Behavior|Input|Output|
|-|-|-|
|Will allow user to add stylist to database|Paul|Paul|
|Will allow user to add customer to database|Adam|Adam|
|Will allow user to link customer to stylist|Paul - Adam| Paul - Adam|
|Will allow user to update customers name| Adam - Adam Titus|Paul - Adam Titus|
|Will allow user to delete customer| Adam| Paul - |

### Known Bugs
* Cannot delete Stylists currently

### Contact Me
You can reach me at adamtitus76@gmail.com or connect with me on [linkedin](www.linkedin.com/in/adam-titus-06740b149).
#### Legal
This is licensed under the MIT license

Copyright (c) 2017 Adam Titus All Rights Reserved.

_If you find a way to monetize this please contact the author_
