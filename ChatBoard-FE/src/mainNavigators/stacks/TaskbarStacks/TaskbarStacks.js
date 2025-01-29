import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import AllTaskBar from '../../../screens/AllTaskBar/AllTaskBar';
import EditTaskBar from '../../../screens/EditTaskBar/EditTaskBar';



const Stack = createNativeStackNavigator();
// @refresh reset
const TaskbarStacks = ({ route }) => {
  return (
    <Stack.Navigator
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen name="TaskBar" component={AllTaskBar} />
      <Stack.Screen name="EditTaskBar" component={EditTaskBar} />
    </Stack.Navigator>
  );
};
export default TaskbarStacks;
