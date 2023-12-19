import 'package:flutter/material.dart';
import 'package:villabeachapp/controllers/users_controller.dart';
import 'package:villabeachapp/pages/users_page/components/users_card.dart';

class ListOfUsers extends StatefulWidget {
  const ListOfUsers({super.key});

  @override
  State<ListOfUsers> createState() => _ListOfUsersState();
}

class _ListOfUsersState extends State<ListOfUsers> {
  _ListOfUsersState();

  @override
  Widget build(BuildContext context) {
    return RefreshIndicator(
      onRefresh: () => UsersController.to.start(),
      child: ListView.builder(
        itemCount: UsersController.to.users.length,
        itemBuilder: (context, index) =>
            UsersCard(user: UsersController.to.users[index]),
      ),
    );
  }
}
