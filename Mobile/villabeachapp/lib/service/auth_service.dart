import 'dart:convert';

import 'package:get/get.dart';
import 'package:http/http.dart';
import 'package:villabeachapp/model/user.dart';
import 'package:villabeachapp/utility/secure_storage.dart';
import 'package:villabeachapp/utility/user_token_handler.dart';
import 'package:villabeachapp/utility/webservice_url.dart';
import 'package:villabeachapp/widgets/snack_auth_error.dart';

class AuthService extends GetxController {
  final Rxn<User?> _user = Rxn<User?>();
  var userIsAuthenticated = false.obs;

  User? get user => _user.value;

  static AuthService get to => Get.find<AuthService>();

  createUser(String email, String password, String name) async {}

  Future login(String email, String password) async {
    final Map<String, dynamic> loginData = {
      'email': email,
      'password': password
    };

    var url = Uri.parse(WebServiceUrl.login);

    var response = await post(
      url,
      body: json.encode(loginData),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseData = json.decode(response.body);

      TokenSecureStore().saveTokens(
          responseData['accessToken'], responseData['refreshToken']);

      _user.value = UserTokenHandler().createUser(responseData['accessToken']);

      userIsAuthenticated.value = true;
    } else {
      SnackAuthError().show(json.decode(response.body)['title']);
    }
  }

  resetPassword(String email) async {}

  logout() async {
    userIsAuthenticated.value = false;
  }
}
