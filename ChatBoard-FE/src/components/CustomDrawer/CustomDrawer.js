/* eslint-disable prettier/prettier */

import { useTheme } from '../../hooks';
import {
    StyleSheet,
    Platform,
    ImageBackground,
    View,

} from 'react-native';
import React from 'react';



export const CustomDrawerContent = props => {
    const { Images } = useTheme()

    return (
        <View
            style={[styles.DrawerCotainer, { flex: 1, backgroundColor: "grey" }]}
        >
        </View>
    );
};

const styles = StyleSheet.create({
    SwitchTab: {
        width: '70%',
        alignItems: 'center',
        flexDirection: 'row',
        justifyContent: 'space-between',
        marginVertical: 12
    },
    settingConainer: {
        width: '85%',
        paddingHorizontal: 5,
    },
    profileArrow: {
        height: '100%',
        width: '20%',
        alignItems: 'center',
        justifyContent: 'center',
    },
    nameConainer: {
        height: '100%',
        width: '70%',
        marginLeft: 5,
        justifyContent: 'center',
    },
    profilePic: {
        height: 50,
        width: 50,
        borderRadius: 25,
        borderColor: 'white',
        borderWidth: 3,
    },
    profileNameContainer: {
        height: '100%',
        width: '80%',
        flexDirection: 'row',
        alignItems: 'center',
        paddingLeft: 2,
    },
    profileTab: {
        height: 60,
        width: '85%',
        borderRadius: 50,

        marginVertical: 16,
        borderWidth: 1,
        flexDirection: 'row',
    },
    profileTabContainer: {
        height: '100%',
        width: '100%',
        flexDirection: 'row',
    },
    logout: {
        position: 'absolute',
        bottom: 10,
        left: 20,
        borderRadius: 25,
    },
    DrawerCotainer: {
        height: '100vh',
        width: '70%',
        alignItems: 'center',
        alignContent: 'center',
        ...Platform.select({
            ios: {
                paddingVertical: 60,
            },
            android: {
                paddingVertical: 25,
            },
        }),
    },
    BlurView: {
        height: '100%',
        width: '100%',
        alignItems: 'center',
        justifyContent: 'center',
    },
    tabImage: {
        height: 22,
        width: 22,
        marginHorizontal: 10,
    },
    tab: {
        height: '100%',
        width: '100%',
        borderRadius: 10,
        paddingHorizontal: 5,
        borderWidth: 1,
        flexDirection: 'row',
        alignItems: 'center',
    },
    tabContainer: {
        height: 40,
        width: '85%',
        marginVertical: 5,
    },
    underLined: {
        fontSize: 14,
        textDecorationLine: 'underline',
        marginVertical: 2,
    },
    image: {
        height: '100%',
        width: '100%',
        objectFit: 'contain',
    },
    headingTab: {
        height: 45,
        width: '100%',
        alignItems: 'center',
        flexDirection: 'row',
    },
    separator: {
        height: 2,
        width: '80%',
        backgroundColor: '#F3F3F3',
        marginBottom: 15,
        marginTop: 20,
    },
    exerciseText: {
        color: 'rgba(38, 50, 56, 0.68)',
        fontSize: 14,
        marginVertical: 5,
    },
    imageContainer: {
        height: 50,
        width: 50,
        borderWidth: 2,
        borderRadius: 50,
        display: 'flex',
        marginLeft: 2,
        justifyContent: 'center',
        alignItems: 'center',
    },
    profileImageStyle: {
        flex: 1,
        width: '100%',
        height: 43,
        borderRadius: 100,
    }
});
