import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Login from '../../../screens/Login/Login';
import SignUp from '../../../screens/SignUp/SignUp';
import Welcome from '../../../screens/Welcome/Welcome';


const Stack = createNativeStackNavigator();
// @refresh reset
const AuthNavigator = ({ route }) => {
  return (
    <Stack.Navigator
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen name="Welcome" component={Welcome} />
      <Stack.Screen name="Login" component={Login} />
      <Stack.Screen name="SignUp" component={SignUp} />
    </Stack.Navigator>
  );
};
export default AuthNavigator;
