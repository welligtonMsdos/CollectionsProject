namespace Collection10Api.Application.Dtos;

public record VinylDto(Guid Guid,
                       string Artist,
                       string Album,
                       int Year,
                       string Photo,
                       decimal Price);

public record VinylCreateDto(string Artist,
                             string Album,
                             int Year,
                             string Photo,
                             decimal Price);

public record VinylUpdateDto(string Artist,
                             string Album,
                             int Year,
                             string Photo,
                             decimal Price);

public record VinylByComboDto(Guid Guid,
                              string Artist);