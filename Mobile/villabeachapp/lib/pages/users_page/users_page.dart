import 'package:flutter/material.dart';
import 'package:villabeachapp/controllers/users_controller.dart';

class UsersPage extends StatefulWidget {
  const UsersPage({super.key});

  @override
  State<UsersPage> createState() => _UsersPageState();
}

class _UsersPageState extends State<UsersPage> {
  final UsersController _controller = UsersController();

  @override
  initState() {
    super.initState();
    _controller.start();
  }

  _start() {
    return Container();
  }

  _loading() {
    return const Center(child: CircularProgressIndicator());
  }

  _onError() {}

  _ready() {
    return RefreshIndicator(
      onRefresh: () => _controller.start(),
      child: ListView.builder(
        itemCount: _controller.users.length,
        itemBuilder: (context, index) {
          var user = _controller.users[index];
          return Column(
            children: <Widget>[
              ListTile(
                leading: CircleAvatar(
                  child: ClipOval(
                    child: Container(
                      child: user.photoURL != null
                          ? Image.network(
                              user.photoURL!,
                              fit: BoxFit.cover,
                            )
                          : const FittedBox(
                              fit: BoxFit.fill,
                              child: Icon(
                                Icons.account_circle,
                                size: 100,
                              ),
                            ),
                    ),
                  ),
                ),
                title: Text(user.name ?? "err"),
                subtitle: Text(user.email ?? 'err'),
              ),
              const Divider()
            ],
          );
        },
      ),
    );
  }

  stateManagement(UsersControllerState state) {
    switch (state) {
      case UsersControllerState.starting:
        return _start();
      case UsersControllerState.loading:
        return _loading();
      case UsersControllerState.onError:
        return _onError();
      case UsersControllerState.ready:
        return _ready();
      default:
        return _start();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text("Usuários"),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => Navigator.pop(context, false),
        ),
      ),
      body: AnimatedBuilder(
        animation: _controller.state,
        builder: (context, child) {
          return stateManagement(_controller.state.value);
        },
      ),
    );
  }
}
