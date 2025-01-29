import { StatusBar, StyleSheet, Text, TouchableOpacity, View, Dimensions } from 'react-native';
import React from 'react';
import { useNavigation } from '@react-navigation/native';
import { APP_COLORS } from '../../utils/colorContants';
import { ACTIVE_OPACITY } from '../../utils/constants';
import { useSelector } from 'react-redux';

const Welcome = () => {
    const navigate = useNavigation();
    const { width, height } = Dimensions.get('window'); // Get device width and height
    const token = useSelector(state => state.token)
    return (
        <>
            <StatusBar
                translucent
                backgroundColor="transparent"
                barStyle="dark-content" // Use "dark-content" for dark text/icons
            />
            <View style={styles.container}>
                <TouchableOpacity
                    activeOpacity={ACTIVE_OPACITY}
                    style={[
                        styles.area,
                        {
                            width: width * 0.7, // 60% of screen width
                            height: width * 0.7, // Make it a square with 60% of screen width
                            borderRadius: (width * 0.7) / 2 // Ensure it's a perfect circle
                        }
                    ]}
                    onPress={() => {
                        if (token) {
                            navigate.navigate("DrawerNavigation");
                        }
                        else {
                            navigate.navigate("Login");
                        }

                    }}
                >
                    <Text style={styles.text}>ChatBoard...</Text>
                </TouchableOpacity>
            </View>
        </>
    );
};

export default Welcome;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: APP_COLORS.gradientLight,
        justifyContent: "center",
        alignItems: "center",
    },
    area: {
        backgroundColor: "#1066B738",
        justifyContent: "center",
        alignItems: "center",
    },
    text: {
        fontSize: 20,
    },
});
