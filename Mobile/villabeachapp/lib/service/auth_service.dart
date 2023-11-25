import 'dart:convert';

import 'package:get/get.dart';
import 'package:http/http.dart';
import 'package:villabeachapp/model/user.dart';
import 'package:villabeachapp/security/secure_storage.dart';
import 'package:villabeachapp/security/user_token_handler.dart';
import 'package:villabeachapp/utility/webservice_url.dart';
import 'package:villabeachapp/security/snack_auth_error.dart';

class AuthService extends GetxController {
  final Rxn<User?> _user = Rxn<User?>();
  var userIsAuthenticated = false.obs;

  @override
  void onInit() {
    super.onInit();

    ever(_user, (User? user) {
      if (user != null) {
        userIsAuthenticated.value = true;
      } else {
        userIsAuthenticated.value = false;
      }
    });
  }

  userAuhenticade() {
    if (user != null) {
      userIsAuthenticated.value = true;
    } else {
      userIsAuthenticated.value = false;
    }
  }

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

  Future logout() async {
    String? token = await TokenSecureStore().getAccessTokens();

    var url = Uri.parse(WebServiceUrl.logout);

    var response = await post(
      url,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token'
      },
    );

    if (response.statusCode != 200) {
      SnackAuthError().show(json.decode("Erro ao deslogar do servidor"));
    }

    _user.value = null;
    TokenSecureStore().deleteTokens();
  }
}
