



# JobAutomationTool

A tool for dividing and automating jobs that consists of large operations.

## Getting Started

Before getting started, there are some requirements for this tool to work. In the JobConsole example, printing a list of objects (CustomerModel) is automated and from now on documentation will continue based on this example.

## Usage

Currently, there are two automating job types included in this project. One is automating by row number and the other one is by date. Records have to be divided into groups. Also if there have to be a date or row number parameter specified according to automating type used. In JobConsole, there are 5000 objects and every 1000 objects divided into groups from 1 to 5. Every group has created date and row numbers from 1 to 1000.

### SettingModel
Properties are specified in app.config of AutomationConsole project. You can make your changes there.
| **Parameter**   | **Parameter Type** | **Default**                                  | **Requirement**                      | **Description**                                                                                                                                                                                                                                                |
|-----------------|--------------------|----------------------------------------------|--------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| GroupStart      | int                | 1                                            | Mandatory                            | Starting group number. Will be processed.                                                                                                                                                                                                                      |
| GroupEnd        | int                | 2                                            | Mandatory                            | Last group number. Will not be processed.                                                                                                                                                                                                                      |
| Action          | string             | PrintListByDate                              | Mandatory                            | Action to do. Must be set in the job console.                                                                                                                                                                                                                  |
| ApplicationPath | string             | ..\\..\\..\\JobConsole\bin\Debug\JobConsole.exe | Mandatory                            | Path of the application. Must include application.exe                                                                                                                                                                                                          |
| DateStart       | DateTime           | 2010-01-01T00:00:00                          | Mandatory (For automating by date)   | Starting date for dividing groups into sub groups. Will be processed.                                                                                                                                                                                          |
| DateFinish      | DateTime           | 2022-12-31T23:59:59                          | Mandatory (For automating by date)   | Last date for dividing groups into sub groups. Will be processed.                                                                                                                                                                                              |
| IncreaseType    | string             | Month                                        | Mandatory (For automating by date)   | Increase type of date. Types: Minute \| Hour \| Day \| Month \| Year                                                                                                                                                                                           |
| RowNumberStart  | int                | 1                                            | Mandatory (For automating by number) | Starting index for dividing groups into sub groups. Will be processed.                                                                                                                                                                                         |
| RowNumberFinish | int                | 1000                                         | Mandatory (For automating by number) | Last index for dividing groups into sub groups. Will be processed.                                                                                                                                                                                             |
| IncreaseAmount  | int                | 1                                            | Mandatory                            | Increase amount to be added from start number or date till the end. This parameter determines how many sub groups will be for each group. For example if StartNumber=1, EndNumber=1000 and IncreaseAmount=100 then there will be 10 subgroups for every group. |

## Screenshots
### Automate By Date
![Automate By Date](https://i.postimg.cc/HkPdKdd1/Automate-By-Date.png)

### Automate By Row Number
![Automate By Row Number](https://i.postimg.cc/4xxdKWBw/Automate-By-Row-Number.png)

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[GNU GPL V3.0](https://choosealicense.com/licenses/gpl-3.0/)
