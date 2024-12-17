
# VORTEX - LIBRERIA PARA LA CONFIGURACION DE USARIOS

## Configuracion de la libreria

Para poder utilizar la libreria es necesario importarla en el archivo de configuracion de la aplicacion, para ello se debe agregar la siguiente linea de codigo:


### Program.cs
```c#

//Esto va en el archivo inyeccion de dependencias
    
    using vortexUserConfig;
    using vortexUserConfig.Config;    

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddVortexUserConfig();
    
        // Se tiene que elegir entre  Roles, Permission
        ConfigUserInit initUserConfig = new ConfigUserInit { 
            ConfigPermission = "Permission",
         };
              
        services.AddSingleton(Options.Create(initUserConfig));
        
        
    }
```

## 