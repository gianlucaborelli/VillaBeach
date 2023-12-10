import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:villabeachapp/model/user_model.dart';
import 'package:villabeachapp/service/user_service.dart';

class UsersController extends GetxController {
  final state =
      ValueNotifier<UsersControllerState>(UsersControllerState.starting);
  List<UserModel> users = [];

  static UsersController get to => Get.find<UsersController>();

  Future start() async {
    state.value = UsersControllerState.loading;
    update();
    try {
      users = await UserService.to.getAllUsers();
      state.value = UsersControllerState.ready;
      update();
    } catch (ex) {
      state.value = UsersControllerState.onError;
      update();
    }
  }
}

enum UsersControllerState { starting, loading, ready, onError }
