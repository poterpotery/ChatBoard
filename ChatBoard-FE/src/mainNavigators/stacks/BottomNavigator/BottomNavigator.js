import React, { useEffect } from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { TouchableWithoutFeedback } from 'react-native'; // Import to disable ripple
import { StyleSheet, View, Text } from 'react-native';
import MessageSvg from '../../../components/Svgs/BottomNavigatorSvgs/MessageSvg';
import SettingsSvg from '../../../components/Svgs/BottomNavigatorSvgs/SettingsSvg';
import HomeSvg from '../../../components/Svgs/BottomNavigatorSvgs/HomeSvg';
import TaskbarStacks from '../TaskbarStacks/TaskbarStacks';
import MessagesStacks from '../MessagesStacks/MessagesStacks';
import SettingsStacks from '../SettingsStacks/SettingsStacks';
import { useSelector } from 'react-redux';

const Tab = createBottomTabNavigator();
export function BottomNavigator({ navigation }) {


    

    return (
        <>
            <Tab.Navigator
                screenOptions={({ route }) => ({
                    headerShown: false,
                    tabBarStyle: [],
                    tabBarLabelStyle: {
                        display: 'none',
                    },
                    tabBarButton: [
                        // 'ProfileNavigation',
                    ].includes(route.name)
                        ? () => null
                        : (props) => (
                            <TouchableWithoutFeedback {...props}>
                                <View style={{justifyContent:"center",alignItems:"center",height:"100%", backgroundColor:"#ECE2E1"}}>{props.children}</View>
                            </TouchableWithoutFeedback>
                        ),
                })}
            >
                <Tab.Screen
                    options={{
                        headerShown: false,
                        lazy: false,
                        tabBarIcon: ({ focused, color }) => (
                            <View style={styles.tabImgContainer}>
                                <HomeSvg isFocused={focused} width={20} height={20} />
                                <Text style={styles.bottomText}>Task</Text>
                            </View>
                        ),
                    }}
                    name="TaskbarStacks"
                    component={TaskbarStacks}
                />
                <Tab.Screen
                    options={{
                        headerShown: false,
                        lazy: false,
                        tabBarIcon: ({ focused, color }) => (
                            <View style={styles.tabImgContainer}>
                                <MessageSvg isFocused={focused} width={20} height={20} />
                                <Text style={styles.bottomText}>Messages</Text>
                            </View>
                        ),
                    }}
                    name="MessagesStacks"
                    component={MessagesStacks}
                />
                <Tab.Screen
                    options={{
                        headerShown: false,
                        lazy: false,
                        tabBarIcon: ({ focused, color }) => (
                            <View style={styles.tabImgContainer}>
                                <SettingsSvg isFocused={focused} width={20} height={20} />
                                <Text style={styles.bottomText}>Settings</Text>
                            </View>
                        ),
                    }}
                    name="SettingsStacks"
                    component={SettingsStacks}
                />
            </Tab.Navigator>
        </>
    );
}

const styles = StyleSheet.create({
    tabImgContainer: {
        width:100,
        justifyContent:"center",alignItems:"center",

        paddingVertical:10
    },
    bottomText:{
        fontSize:10,
         fontFamily: "Ubuntu-Bold",
    }
});
