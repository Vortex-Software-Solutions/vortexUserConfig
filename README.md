
# VORTEX - LIBRERIA PARA LA CONFIGURACION DE USARIOS

## Configuracion de la libreria

Para poder utilizar la libreria es necesario importarla en el archivo de configuracion de la aplicacion, para ello se debe agregar la siguiente linea de codigo:


### Program.cs
```c#

    
    using Microsoft.Extensions.Options;
    using vortexUserConfig;
    using vortexUserConfig.Config;

    public void ConfigureServices(IServiceCollection services)
    {
        //Esto va en el archivo inyeccion de dependencias
        services.AddVortexUserConfig(builder.Configuration);
    
        // Se tiene que elegir entre  Roles, Permission
        ConfigUserInit initUserConfig = new ConfigUserInit { 
            ConfigPermission = "Permission",
         };
              
        services.AddSingleton(Options.Create(initUserConfig));
        
        
    }
```

## appsettings.json

``` json

  "Authentication": {
    "Key": "" , // Agregar la llave del servicio
    "Issuer": "", //Agregar el issuer
    "Audience": "" //Agregar la audiencia
  },

```
