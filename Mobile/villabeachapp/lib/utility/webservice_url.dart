class WebServiceUrl {
  static const String baseUrl = 'http://10.0.2.2:5251/api';

  static const String baseUserUrl = '$baseUrl/users';

  static const String login = '$baseUserUrl/login';
  static const String register = '$baseUserUrl/register';
}
