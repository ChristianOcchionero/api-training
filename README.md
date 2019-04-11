# Uniconta API Training
This repository contains various training material for the Uniconta API.

## standalone:
  ### case1: Uniconta login skærm
    Task: create connectivity
    Task: verify login

  ### case2: Uniconta API wrapper
    Task: create a wrapper class for the Uniconta API with the following methods:
      init method
      login/logout method
      InitializeCompany to select default company for the user or first company and check wether the user has access to a company
      get CrudAPI method, to create a new crudAPI.

  ### case3: Data population
    Task: Create a button to populate Uniconta with some data. E.g. Items, Debtors or Orders.
    
  ### case4: Userdefined fields and tables
    Task: Create button to create table(tableheader)
    Task: Create button to populate the table with fields

## IPluginBase:
  ### case5: Uniconta indlæsningsværktøj med valg af fil (ImportData.csv)
    Task: get lines from file
    Task: Create Uniconta entities (Sales orderes) from file data

## IContentbase:
  ### case6: Antal solgt af en item
    Task: Create GUI with overview of items
    Task: Calculate the number of times an item has been sold

## PageEventsBase:
  ### Case7: Using events to hook into the invoice flow, creating purchase orders when a debtorOrder is invoiced
    Task: Hook into the flow of the correct event
    
## ClientTools
  ### Case8: Set quantity of freight to the sum of the quantity of items
    Task: Hook into the flow of inputting the orderline
    Task: Get the orderlines from the page field
    Task: Set the quantity to the sum of the quantity of the lines

## Localization:
  ### Localization: Use the Localization entity to work with Uniconta labels
    Task: Find localized label
    Task: Override label

## JournalLoader:
  ### JournalLoader: Load data into Uniconta and use the posting API to post the journal lines
    Task: Load data from file
    Task: Insert data into Uniconta
    Task: Post (bogør) data
