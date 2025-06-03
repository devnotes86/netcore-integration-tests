appsettings.json file is exclided from Git:

{
   
    "ConnectionStrings": {
      "WriteDb": "Host=<master_db_ip>;Port=5432;Database=heavymetal_db1;Username=rw_user;Password=<rw_user_password>",
      "ReadDb": "Host=<replica_db_ip>;Port=5432;Database=heavymetal_db1;Username=ro_user2;Password=<ro_user_password>"
    },



  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"

}
