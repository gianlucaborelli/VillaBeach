import 'dart:io';

class WebServiceUrl {
  static String get baseUrl {
    if (Platform.isAndroid) {
      return 'https://villabeachwebservice1-1wjapvsj.b4a.run/api';
    } else if (Platform.isWindows) {
      // URL para Windows
      return 'https://villabeachwebservice1-1wjapvsj.b4a.run/api';
    } else {
      return 'https://villabeachwebservice1-1wjapvsj.b4a.run/api';
    }
  }

  static String baseUserUrl = '$baseUrl/users';

  static String login = '$baseUserUrl/login';
  static String refresToken = '$baseUserUrl/refresh-token';
  static String register = '$baseUserUrl/register';
  static String logout = '$baseUserUrl/logout';
  static String settings = '$baseUserUrl/settings';
}
