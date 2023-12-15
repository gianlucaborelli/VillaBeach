import 'dart:convert';

import 'package:dio/dio.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/model/user.dart';
import 'package:villabeachapp/model/user_settings.dart';
import 'package:villabeachapp/security/secure_storage.dart';
import 'package:villabeachapp/security/user_token_handler.dart';
import 'package:villabeachapp/utility/dio_interceptor/onerror_interceptor.dart';
import 'package:villabeachapp/utility/dio_interceptor/onrequest_interceptor.dart';
import 'package:villabeachapp/utility/webservice_url.dart';
import 'package:villabeachapp/security/snack_auth_error.dart';

class AuthService extends GetxController {
  late final Dio _dio;
  final Rxn<User?> _user = Rxn<User?>();
  final Rxn<UserSettings?> _settings = Rxn<UserSettings?>();
  var userIsAuthenticated = false.obs;

  AuthService() {
    _dio = Dio(BaseOptions(
      connectTimeout: const Duration(milliseconds: 5000),
    ));
    _dio.interceptors.add(OnRequestInterceptor());
    _dio.interceptors.add(OnErrorInterceptor());
  }

  @override
  void onInit() {
    super.onInit();
    ever(_user, (User? user) => userIsAuthenticated.value = user != null);
  }

  User? get user => _user.value;
  UserSettings? get settings => _settings.value;
  static AuthService get to => Get.find<AuthService>();

  createUser(String email, String confirmPassword, String password,
      String name) async {
    final Map<String, dynamic> registerData = {
      'name': name,
      'email': email,
      'password': password,
      "confirmPassword": password
    };

    var response = await _dio.post(
      WebServiceUrl.register,
      data: registerData,
    );

    if (response.statusCode == 200) {
      SnackAuthError()
          .show("Foi enviado um email de confirmação para o e-mail cadastrado");
    } else {
      SnackAuthError().show(json.decode(response.data)['title']);
    }
  }

  Future login(String email, String password) async {
    final Map<String, dynamic> loginData = {
      'email': email,
      'password': password
    };

    var response = await _dio.post(WebServiceUrl.login, data: loginData);

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseData = response.data;

      TokenSecureStore().saveTokens(
          responseData['accessToken'], responseData['refreshToken']);

      _user.value = UserTokenHandler().createUser(responseData['accessToken']);
      _settings.value = UserSettings.fromJson(responseData['settings']);

      userIsAuthenticated.value = true;
    } else {
      SnackAuthError().show(json.decode(response.data)['title']);
    }
  }

  Future refreshToken() async {
    final Map<String, dynamic> refreshTokenData = {
      'accessToken': TokenSecureStore().getAccessTokens(),
      'refresToken': TokenSecureStore().getRefreshTokens()
    };

    var response =
        await _dio.post(WebServiceUrl.refresToken, data: refreshTokenData);

    if (response.statusCode == 200) {
      final Map<String, dynamic> responseData = response.data;

      TokenSecureStore().saveTokens(
          responseData['accessToken'], responseData['refreshToken']);
    } else {
      SnackAuthError().show(json.decode(response.data)['title']);
    }
  }

  resetPassword(String email) async {}

  Future logout() async {
    var response = await _dio.post(
      WebServiceUrl.logout,
    );

    if (response.statusCode != 200) {
      SnackAuthError().show("Erro ao deslogar do servidor");
    }

    _user.value = null;
    TokenSecureStore().deleteTokens();
  }
}
