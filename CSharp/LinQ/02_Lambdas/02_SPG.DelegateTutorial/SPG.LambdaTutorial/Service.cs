using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.LambdaTutorial
{
    public class Service
    {
        public ServiceResultDto ServiceBase(
            Func<ServiceDto, ServiceResultDto> init, 
            Func<ServiceDto, ServiceResultDto> validation, 
            Func<ServiceDto, ServiceResultDto> process)
        {
            ServiceResultDto resultDto = new ServiceResultDto();
            ServiceDto dto = new ServiceDto();

            try
            {
                // Init-Vorbereitung
                resultDto = init(dto);
                // Init-Nachbereitung
            }
            catch (Exception)
            { }

            try
            {
                // Validation-Vorbereitung
                resultDto = validation(dto);
                // Validation-Nachbereitung
            }
            catch (Exception)
            { }

            try
            {
                // Process-Vorbereitung
                resultDto = process(dto);
                // Process-Nachbereitung
            }
            catch (Exception)
            { }

            return resultDto;
        }

        public ServiceResultDto ServiceMethod01<ServiceDto>(ServiceDto dto)
        {
            ServiceResultDto resultDto = ServiceBase(
                e =>
                {
                    int id = e.Id;
                    return new ServiceResultDto();
                },
                e =>
                {
                    int id = e.Id;
                    return new ServiceResultDto();
                },
                e =>
                {
                    int id = e.Id;
                    return new ServiceResultDto();
                });

            return resultDto;
        }
    }

    public class ServiceDto
    {
        public int Id { get; set; }
    }

    public class ServiceResultDto
    {
        public int Id { get; set; }
    }
}
