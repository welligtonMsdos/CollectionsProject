namespace CollectionDomain.Dtos.Users;

public record UserDto(
    string _id,
    string Name,
    string Email
);

public record UserCreateDto(
    string Name,
    string Email,
    string Password
);

public record UserUpdateDto(
    string Name,
    string Email
);

public record UserLoginDto(
    string Email,
    string Password
);

public record UserDataLoginDto(
    UserDto User,
    string Token
);