import 'package:dio/dio.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/model/user_model.dart';
import 'package:villabeachapp/utility/dio_interceptor/onerror_interceptor.dart';
import 'package:villabeachapp/utility/dio_interceptor/onrequest_interceptor.dart';
import 'package:villabeachapp/utility/message_snackbar.dart';
import 'package:villabeachapp/utility/webservice_url.dart';

class UserService extends GetxController {
  late final Dio _dio;

  UserService() {
    _dio = Dio(BaseOptions(
      connectTimeout: const Duration(milliseconds: 5000),
    ));
    _dio.interceptors.add(OnRequestInterceptor());
    _dio.interceptors.add(OnErrorInterceptor());
  }

  static UserService get to => Get.find<UserService>();

  Future<List<UserModel>> getAllUsers() async {
    try {
      var response = await _dio.get(WebServiceUrl.baseUserUrl);
      var list = response.data as List;
      return list.map((json) => UserModel.fromJson(json)).toList();
    } on DioException catch (e) {
      MessageSnackBar(message: e.response!.data.toString()).show();
    } catch (e) {
      MessageSnackBar(message: e.toString()).show();
    }
    return [];
  }
}
