import 'dart:convert';

import 'package:villabeachapp/model/user.dart';

class UserTokenHandler {
  User createUser(String response) {
    final decoded = json.decode(decodeBase64(response.split(".")[1]));

    return User(
        email: decoded[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
        name: decoded[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        role: decoded[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
        userId: decoded[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]);
  }

  String decodeBase64(String toDecode) {
    String res;
    try {
      while (toDecode.length * 6 % 8 != 0) {
        toDecode += "=";
      }
      res = utf8.decode(base64.decode(toDecode));
    } catch (error) {
      throw Exception("decodeBase64([toDecode=$toDecode]) \n\t\terror: $error");
    }
    return res;
  }
}
