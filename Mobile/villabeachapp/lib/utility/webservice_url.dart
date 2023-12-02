import 'dart:io';

class WebServiceUrl {
  static String get baseUrl {
    if (Platform.isAndroid) {
      return 'http://10.0.2.2:5251/api';
    } else if (Platform.isWindows) {
      // URL para Windows
      return 'http://localhost:5251/api';
    } else {
      return 'http://10.0.2.2:5251/api';
    }
  }

  static String baseUserUrl = '$baseUrl/users';

  static String login = '$baseUserUrl/login';
  static String register = '$baseUserUrl/register';
  static String logout = '$baseUserUrl/logout';
  static String settings = '$baseUserUrl/settings';
}
